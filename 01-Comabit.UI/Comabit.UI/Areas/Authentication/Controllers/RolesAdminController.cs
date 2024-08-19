// <copyright file="RolesAdminController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.DL;
    using Comabit.UI.Areas.Authentication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Authentication")]
    [Authorize]
    public class RolesAdminController : BaseAuthenticationController
    {
        public RolesAdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ILogger<RolesAdminController> logger) : base(signInManager, roleManager, logger)
        {
        }

        [HasPermission(Permissions.RoleList)]
        public ActionResult Index()
        {
            List<ApplicationRole> roles;

            if (User.IsInRole("SuperAdmin"))
            {
                roles = RoleManager.Roles.ToList();
            }
            else
            {
                roles = RoleManager.Roles
                    .Where(r => r.Name != "SuperAdmin" && r.Name != "Agent" && r.Name != "Senior Agent")
                    .OrderBy(r => r.Id).ToList();
            }
            
            List<RoleListViewModel> roleList = new List<RoleListViewModel>();

            foreach (ApplicationRole role in roles)
            {
                roleList.Add(new RoleListViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                });
            }

            return View(roleList);
        }

        [HasPermission(Permissions.RoleCreate)]
        public ActionResult Create()
        {
            RoleViewModel roleModel = new RoleViewModel();

            return View(roleModel);
        }

        [HasPermission(Permissions.RoleCreate)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = new ApplicationRole(roleViewModel.Name);

                // Save the new Description property:
                role.Description = roleViewModel.Description;

                IdentityResult roleresult = await RoleManager.CreateAsync(role);

                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First().Description);

                    return View("Create", roleViewModel);
                }

                // db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Create", roleViewModel);
        }

        [HasPermission(Permissions.RoleEdit)]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            roleModel.Description = role.Description;

            return View("Edit", roleModel);
        }

        [HasPermission(Permissions.RoleEdit)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;

                // Update the new Description property:
                role.Description = roleModel.Description;
                await RoleManager.UpdateAsync(role);
                
                // db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Edit", roleModel);
        }

        [HasPermission(Permissions.RoleDelete)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return View("Delete", role);
        }

        [HasPermission(Permissions.RoleDelete)]
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var role = await RoleManager.FindByIdAsync(id);
                
                if (role == null)
                {
                    return NotFound();
                }

                IdentityResult result;

                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().Description);

                    return View("Delete", role);
                }

                return RedirectToAction("Index");
            }
            return View("Delete", id);
        }
    }
}