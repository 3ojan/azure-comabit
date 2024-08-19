// <copyright file="PermissionController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.DL;
    using Comabit.DL.Interfaces;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Authentication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Authentication")]
    public class PermissionController : BaseAuthenticationController
    {
        private readonly IPermissionService _permissionService;

        private readonly UserManager<ApplicationUser> _userService;

        public PermissionController(UserManager<ApplicationUser> userService, IPermissionService permissionService, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ILogger<PermissionController> logger) : base(signInManager, roleManager, logger)
        {
            this._permissionService = permissionService;
            this._userService = userService;
        }

        [HasPermission(Permissions.PermissionList)]
        public ActionResult Index()
        {
            PermissionListViewModel vm = new PermissionListViewModel();

            vm.Permissions = this._permissionService.GetAll()
                .OrderBy(p => p.GroupName)
                .ThenBy(p => p.Description)
                .ToList()
                .Select(p => new PermissionViewModel(p))
                .ToList();

            List<ApplicationRole> roles;

            if (User.IsInRole("SuperAdmin"))
            {
                roles = RoleManager.Roles.ToList();
            }
            else
            {
                roles = RoleManager.Roles.Where(r => r.Name != "SuperAdmin").OrderBy(r => r.Id).ToList();
            }

            foreach (ApplicationRole role in roles)
            {
                List<int> assignedPermissions = _permissionService.GetPermissionForRole(role.Id).ToList().Select(p => p.ApplicationPermissionId).ToList();

                vm.Roles.Add(new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    AssignedPermissions = vm.Permissions.Where(p => assignedPermissions.Contains(p.Id)).ToList()
                });
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [HasPermission(Permissions.PermissionEdit)]
        public async Task<ActionResult> SetPermission(string roleId, Guid permissionGuid, bool on)
        {
            ApplicationPermission permission = _permissionService.GetPermission(permissionGuid);

            if (permission == null)
            {
                return NotFound();
            }

            ApplicationRole role = await RoleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            if (on)
            {
                permission.Roles.Add(role);

                int changes = await _permissionService.SaveAsync();

                if (changes > 0)
                {
                    await UpdateRoleSecurity(roleId);
                }

                return new JsonNetResult(new
                {
                    status = "added"
                });
            }
            else
            {
                permission.Roles.Remove(role);

                int changes = await this._permissionService.SaveAsync();

                if (changes > 0)
                {
                    await UpdateRoleSecurity(roleId);
                }

                return new JsonNetResult(new
                {
                    status = "deleted"
                });
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> InitAllPermissions()
        {
            List<ApplicationPermission> existingPermissions = this._permissionService.GetAll().ToList();

            foreach (var permission in Permissions.AllPermissions)
            {
                if (!existingPermissions.Any(ep => ep.Value == permission.Value))
                {
                    this._permissionService.AddPermission(new ApplicationPermission() { Value = permission.Value, Description = permission.Description, GroupName = permission.GroupName, Guid = Guid.NewGuid() });
                }
                else
                {
                    var existingPermission = existingPermissions.Where(ep => ep.Value == permission.Value).First();

                    existingPermission.Description = permission.Description;
                    existingPermission.GroupName = permission.GroupName;
                }
            }

            await this._permissionService.SaveAsync();


            ApplicationRole superAdminRole = await RoleManager.FindByNameAsync("SuperAdmin");
            List<ApplicationPermission> allExistingPermissionIds = this._permissionService.GetAll().ToList();
            List<ApplicationPermission> assignedSuperAdminPermissionIds = this._permissionService.GetPermissionForRole(superAdminRole.Id).ToList();

            foreach (var permission in allExistingPermissionIds.Except(assignedSuperAdminPermissionIds))
            {
                superAdminRole.Permissions.Add(permission);
            }

            int changes = await this._permissionService.SaveAsync();

            if (changes > 0)
            {
                await UpdateRoleSecurity(superAdminRole);
            }

            return Content("ok");
        }

        [HasPermission(Permissions.PermissionEdit)]
        public async Task<ActionResult> ResetAllPermissionGuids()
        {
            List<ApplicationPermission> existingPermissions = this._permissionService.GetAll().ToList();

            foreach (var permission in existingPermissions)
            {
                permission.Guid = Guid.NewGuid();
            }

            await this._permissionService.SaveAsync();

            return Content("ok");
        }

        public async Task<bool> UpdateRoleSecurity(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);

            return await UpdateRoleSecurity(role);
        }

        /// <summary>
        /// http://stackoverflow.com/questions/24286489/how-do-i-forcefully-propagate-role-changes-to-users-with-asp-net-identity-2-0-1
        /// </summary>
        public async Task<bool> UpdateRoleSecurity(ApplicationRole role)
        {
            if (role == null)
            {
                return false;
            }

            var users = new List<ApplicationUser>();

            try
            {
                foreach (var user in this._userService.Users.ToList())
                {
                    if (await this._userService.IsInRoleAsync(user, role.Name))
                    {
                        // bisherige Claims des Users als ungültig markieren:
                        user.ClaimsSecurityStamp = Guid.NewGuid().ToString();

                        //ClaimsIdentity claimsIdentity =  await user.GenerateUserIdentityAsync(UserManager, RoleManager, permissionService);
                        await this._userService.UpdateAsync(user);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "UpdateRoleSecurity", null);

                return false;
            }

            return true;
        }
    }
}