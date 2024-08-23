// <copyright file="AccountController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.BL.Company;
    using Comabit.BL.Geo;
    using Comabit.BL.Identity;
    using Comabit.BL.Identity.Dto;
    using Comabit.BL.Mail;
    using Comabit.BL.Mail.DTO;
    using Comabit.BL.Porfolio;
    using Comabit.DL;
    using Comabit.DL.Interfaces;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Admin.Models;
    using Comabit.UI.Areas.Authentication.Models;
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using Comabit.UI.Models.Portfolio;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Area("Authentication")]
    public class AccountController : BaseAuthenticationController
    {
        private readonly MailSettings _mailSettings;

        private MailManager MailManager
        {
            get
            {
                return new MailManager(_mailSettings);
            }
        }

        private readonly IPermissionService _permissionService;

        private readonly PortfolioManager _portfolioManager;

        private readonly GeoManager _geoManager;

        private readonly UserManager _userManager;

        private readonly CompanyManager _companyManager;

        public AccountController(CompanyManager companyManager, UserManager userManager, GeoManager geoManager, PortfolioManager portfolioManager, IPermissionService permissionService, IOptions<MailSettings> mailSettings, ILogger<AccountController> logger, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager) : base(signInManager, roleManager, logger)
        {
            this._mailSettings = mailSettings.Value;
            this._permissionService = permissionService;
            this._portfolioManager = portfolioManager;
            this._geoManager = geoManager;
            this._userManager = userManager;
            this._companyManager = companyManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = await _userManager.GetUserByEMail(model.EMail);

            if (user == null)
            {
                ModelState.AddModelError("", "Ungültige Anmeldedaten.");

                return View(model);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                ClaimsIdentity ident = await _userManager.GenerateUserIdentityAsync(user, RoleManager, _permissionService, model.RememberMe);

                await SignInManager.SignInWithClaimsAsync(user, new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    IssuedUtc = DateTime.Now,
                    ExpiresUtc = DateTime.Now.AddMinutes(30),
                }, ident.Claims);

                user.LastLogin = DateTime.Now;
                await _userManager.Update(user);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToLocal(returnUrl);
                }

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            if (result.IsLockedOut)
            {
                //var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);

                //var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);

                //var message = new Message(new string[] { userModel.Email }, "Locked out account information", content, null);

                //await _emailSender.SendEmailAsync(message);

                ModelState.AddModelError("", "The account is locked out");

                return View("AccountLocked", model);
            }
            else
            {
                ModelState.AddModelError("", "Ungültige Anmeldedaten.");

                return View();
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> RegisterSeller()
        {
            return View("RegisterSeller", new RegisterSellerViewModel()
            {
                PortfolioAreas = this.Mapper.Map<ICollection<AreaViewModel>>(await _portfolioManager.RetrievePortfolioAreas(ShowAdditionalTags: true)),
                States = this.Mapper.Map<ICollection<StateViewModel>>(await _geoManager.GetStates()),
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSeller(RegisterSellerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await this._userManager.RegisterSeller(this.Mapper.Map<RegisterSellerItem>(model));

                    string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("RegisterConfirm", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                    MailRequest request = new MailRequest()
                    {
                        ToEmail = model.EMail,
                        Subject = "Comabit Baustoffmanager Registierung",
                        Body = await this.RenderViewAsync("RegisterMail", new RegisterMailViewModel(user.FullName, callbackUrl)),
                    };

                    await MailManager.SendEmailAsync(request);

                    return this.View("RegisterOk", model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            model.States = this.Mapper.Map<ICollection<StateViewModel>>(await _geoManager.GetStates());

            return this.View("RegisterSeller", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonNetResult> AddCommunity(RegisterSellerViewModel viewModel)
        {
            if (viewModel.Communities == null)
            {
                viewModel.Communities = new List<CommunityViewModel>();
            }

            viewModel.Communities.Add(this.Mapper.Map<CommunityViewModel>(await _geoManager.GetCommunity(viewModel.NewCommunityId)));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_SellerCommunitiesEditor", viewModel),
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonNetResult> RemoveCommunity(RegisterSellerViewModel viewModel)
        {
            viewModel.Communities.Remove(viewModel.Communities.Where(c => c.Id == viewModel.NewCommunityId).FirstOrDefault());

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_SellerCommunitiesEditor", viewModel),
            });
        }

        [AllowAnonymous]
        public ActionResult RegisterBuyer()
        {
            return View("RegisterBuyer", new RegisterBaseViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterBuyer(RegisterBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await this._userManager.RegisterBuyer(this.Mapper.Map<RegisterBaseItem>(model));

                    string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("RegisterConfirm", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                    MailRequest request = new MailRequest()
                    {
                        ToEmail = model.EMail,
                        Subject = "Comabit Baustoffmanager Registierung",
                        Body = await this.RenderViewAsync("RegisterMail", new RegisterMailViewModel(user.FullName, callbackUrl)),
                    };

                    await MailManager.SendEmailAsync(request);

                    return this.View("RegisterOk", model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return this.View("RegisterBuyer", model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> RegisterConfirm(string userId, string code)
        {
            ApplicationUser user = await _userManager.GetUser(userId);

            var result = await this._userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                logger.LogError(result.Errors.FirstOrDefault().Description);

                return this.View("Error");
            }

            ClaimsIdentity ident = await _userManager.GenerateUserIdentityAsync(user, RoleManager, _permissionService);

            ICollection<string> admins = (await _userManager.GetAllAdmins()).Select(a => a.Email).ToList();

            CompanyViewModel companyViewModel = this.Mapper.Map<CompanyViewModel>(this._companyManager.GetForUser(userId));
            companyViewModel.MainUser = user;

            MailRequest request = new MailRequest()
            {
                ToEmail = admins.Aggregate((a, b) => a + ";" + b),
                Subject = "Comabit Baustoffmanager Neue Registrierungen",
                Body = await this.RenderViewAsync("NewRegistrationMail", companyViewModel),
            };

            await MailManager.SendEmailAsync(request);

            await SignInManager.SignInWithClaimsAsync(user, new AuthenticationProperties
            {
                IsPersistent = false,
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30),
            }, ident.Claims);

            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;

                if (!string.IsNullOrEmpty(model.Email))
                {
                    user = await _userManager.GetUserByEMail(model.Email);
                }

                if (user == null && !string.IsNullOrEmpty(model.Email))
                {
                    user = await _userManager.GetUserByEMail(model.Email);
                }

                if (user == null || !user.EmailConfirmed)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                ForgotPasswordEmailViewModel mailViewModel = new ForgotPasswordEmailViewModel()
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    EMail = user.Email,
                    SetPasswortUrl = callbackUrl,
                };

                MailRequest request = new MailRequest()
                {
                    ToEmail = user.Email,
                    Subject = "Comabit Baustoffmanager Passwort zurücksetzen",
                    Body = await this.RenderViewAsync("ForgotPasswordEmail", mailViewModel),
                };

                await MailManager.SendEmailAsync(request);

                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string userId)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(userId))
            {
                View("Error");
            }

            return View(new ResetPasswordViewModel() { Code = code, UserId = userId });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUser(model.UserId);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                ClaimsIdentity ident = await _userManager.GenerateUserIdentityAsync(user, RoleManager, _permissionService, false);

                await SignInManager.SignInWithClaimsAsync(user, new AuthenticationProperties
                {
                    IsPersistent = false,
                    IssuedUtc = DateTime.Now,
                    ExpiresUtc = DateTime.Now.AddMinutes(30),
                }, ident.Claims);

                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(result);

            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await SignInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}