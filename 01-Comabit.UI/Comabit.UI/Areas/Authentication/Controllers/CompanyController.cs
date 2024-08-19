// <copyright file="CompanyController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.BL.Company;
    using Comabit.BL.Company.Dto;
    using Comabit.DL;
    using Comabit.UI.Areas.Admin.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Users.Infrastructure;

    [Authorize]
    [Area("Authentication")]
    public class CompanyController : BaseAuthenticationController
    {
        private readonly CompanyManager _companyManager;


        public CompanyController(CompanyManager companyManager, ILogger<CompanyController> logger, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager) : base(signInManager, roleManager, logger)
        {
            this._companyManager = companyManager;
        }

        public ActionResult Edit()
        {
            return View(this.Mapper.Map<CompanyViewModel>(this._companyManager.GetForUser(User.GetUserId())));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this._companyManager.Update(this.Mapper.Map<CompanyItem>(viewModel));

                return View("EditOk");
            }

            return View(viewModel);
        }
    }
}