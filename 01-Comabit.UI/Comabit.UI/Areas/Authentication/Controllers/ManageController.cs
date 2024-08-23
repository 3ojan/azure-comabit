// <copyright file="ManageController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.BL.Identity;
    using Comabit.DL;
    using Comabit.UI.Areas.Authentication.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Authentication")]
    [Route("Manage")]
    [Authorize]
    public class ManageController : BaseAuthenticationController
    {
        private readonly UserManager _userManager;

        public ManageController(UserManager userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ILogger<ManageController> logger) : base(signInManager, roleManager, logger)
        {
            this._userManager = userManager;
        }

        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            ApplicationUser user = await this._userManager.GetUser(User.GetUserId());

            var model = new IndexViewModel
            {
                HasPassword = await HasPassword(),
                User = user
            };

            return View(model);
        }

        [Route("Edit")]
        public async Task<ActionResult> Edit()
        {
            var user = await this._userManager.GetUser(User.GetUserId());

            return View(new EditProfileViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
            });
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProfileViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await this._userManager.GetUser(User.GetUserId());

            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // user.Email = vm.Email;
                user.Firstname = vm.Firstname;
                user.Lastname = vm.Lastname;
                user.EmailConfirmed = true;

                if (vm.ChangePassword)
                {
                    PasswordVerificationResult passwordMatch = this._userManager.VerifyHashedPassword(user, user.PasswordHash, vm.NewPassword);

                    if (passwordMatch == PasswordVerificationResult.Success)
                    {
                        ModelState.AddModelError("", "Das neue Kennwort darf nicht mit dem alten übereinstimmen.");
                    }
                    else
                    {
                        var result = await this._userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);

                        if (result.Succeeded)
                        {
                            user.PasswordChangeDate = DateTime.Now;

                            await this._userManager.Update(user);
                            await RefreshIdentitySession(user);
                        }
                        else
                        {
                            AddErrors(result);

                            return View(vm);
                        }
                    }
                }
                else
                {
                    await this._userManager.Update(user);
                    //await RefreshIdentitySession(user);
                }

                return View("EditSuccess");
            }

            return View(vm);
        }

        [Route("Claims")]
        public ActionResult Claims()
        {
            IIdentity user = User.Identity;
            ClaimsIdentity ident = User.Identity as ClaimsIdentity;

            return View(ident.Claims);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private async Task<bool> HasPassword()
        {
            var user = await this._userManager.GetUser(User.GetUserId());

            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        public async Task RefreshIdentitySession(ApplicationUser user)
        {
            // kill old cookie
            await SignInManager.SignOutAsync();

            // sign in again
            await SignInManager.SignInAsync(user, new AuthenticationProperties()
            {
                IsPersistent = User.HasRememberMe(),
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30)
            });
        }
    }
}