// <copyright file="UsersAdminController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.BL.Identity;
    using Comabit.DL;
    using Comabit.UI.Areas.Authentication.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Authentication")]
    public class UsersAdminController : BaseAuthenticationController
    {
        private readonly UserManager _userManager;

        private readonly UserManager<ApplicationUser> _userService;

        public UsersAdminController(UserManager<ApplicationUser> _userService, UserManager userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ILogger<UsersAdminController> logger) : base(signInManager, roleManager, logger)
        {
            this._userManager = userManager;
            this._userService = _userService;
        }

        [HasPermission(Permissions.UserList)]
        public async Task<ActionResult> Index()
        {
            List<ApplicationUser> users = await _userManager.GetAll();

            ListUsersViewModel uvm = new ListUsersViewModel()
            {
                Users = new List<UserViewModel>()
            };

            foreach (ApplicationUser user in users)
            {
                uvm.Users.Add(new UserViewModel()
                {
                    User = user,
                    Roles = await _userManager.GetRole(user),
                });
            }

            if (User.IsInRole("SuperAdmin"))
            {
                uvm.Users = uvm.Users.ToList();
            }
            else
            {
                uvm.Users = uvm.Users
                       .Where(m => !m.Roles.Contains(Roles.SuperAdmin))
                       .ToList();
            }

            return View(uvm);
        }

        [HasPermission(Permissions.UserCreate)]
        public ActionResult Create()
        {
            CreateUserViewModel createUser = new CreateUserViewModel();

            createUser.RolesList = RoleManager.Roles
                .Where(r => !this.User.IsInRole("SuperAdmin") ? r.Name != "SuperAdmin" : true)
                .ToList().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name
                });

            return View(createUser);
        }

        [HasPermission(Permissions.UserCreate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    Firstname = vm.Firstname,
                    Lastname = vm.Lastname,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now,
                };

                // Then create:
                var adminresult = await this._userService.CreateAsync(user, vm.Password);

                // Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    List<string> selectedRole = new List<string>() { vm.SelectedRole }.Where(r => r != null).ToList();
                    IdentityResult result;

                    if (selectedRole.Count > 0)
                    {
                        result = await _userService.AddToRolesAsync(user, selectedRole.ToArray<string>());
                        
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().Description);
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First().Description);
                }
            }

            vm.RolesList = RoleManager.Roles.ToList()
                .Where(r => !this.User.IsInRole("SuperAdmin") ? r.Name != "SuperAdmin" : true)
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name
                });

            return View(vm);
        }

        [HasPermission(Permissions.UserEdit)]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await this._userService.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await this._userService.GetRolesAsync(user);

            var roles = RoleManager.Roles.ToList()
               .Where(r => !this.User.IsInRole("SuperAdmin") ? r.Name != "SuperAdmin" : true).ToList();

            return View(new EditUserViewModel(user, userRoles, roles));
        }

        [HasPermission(Permissions.UserEdit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel vm)
        {
            var user = await this._userService.FindByIdAsync(vm.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await this._userService.GetRolesAsync(user);

            if (ModelState.IsValid)
            {
                user.UserName = vm.Email;
                user.Email = vm.Email;
                user.Firstname = vm.Firstname;
                user.Lastname = vm.Lastname;

                List<string> selectedRole = new List<string>() { vm.SelectedRole }.Where(r => r != null).ToList();
                IdentityResult result;

                if (selectedRole.Count > 0)
                {
                    result = await this._userService.AddToRolesAsync(user, selectedRole.Except(userRoles).ToArray<string>());

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First().Description);

                        return View();
                    }
                }

                result = await this._userService.RemoveFromRolesAsync(user, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().Description);

                    return View();
                }

                //int changes = db.SaveChanges();

                //if (changes > 0)
                //{
                //    await UserManager.UpdateSecurityStampAsync(user.Id);
                //}

                await this._userService.UpdateSecurityStampAsync(user);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Es ist ein Fehler aufgetreten.");

            vm.RolesList = RoleManager.Roles.ToList()
                .Where(r => !this.User.IsInRole("SuperAdmin") ? r.Name != "SuperAdmin" : true)
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name
                });

            return View(vm);
        }

        [HasPermission(Permissions.UserDelete)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await this._userService.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HasPermission(Permissions.UserDelete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var user = await this._userService.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var result = await this._userService.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().Description);

                    return View();
                }

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
