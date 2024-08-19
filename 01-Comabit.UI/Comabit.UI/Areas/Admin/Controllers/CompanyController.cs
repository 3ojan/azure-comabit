// <copyright file="CompanyController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Admin.Controllers
{
    using AutoMapper;
    using Comabit.BL.Company;
    using Comabit.BL.Company.Dto;
    using Comabit.DL;
    using Comabit.DL.Interfaces;
    using Comabit.UI.Areas.Admin.Models;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Admin")]
    public class CompanyController : BaseController
    {
        private CompanyManager _companyManager;
        private UserManager<ApplicationUser> _userManager;

        public CompanyController(CompanyManager companyManager, UserManager<ApplicationUser> userManager, ILogger<CompanyController> logger) : base(logger)
        {
            this._companyManager = companyManager;
            this._userManager = userManager;
        }

        [HasPermission(Permissions.CompanyList)]
        public async Task<ActionResult> Index()
        {
            ICollection<CompanyItem> buyersItems = await this._companyManager.GetAllBuyer();
            ICollection<SellerItem> sellerItems = await this._companyManager.GetAllSeller();

            ICollection<CompanyViewModel> buyersViewModel = this.Mapper.Map<ICollection<CompanyViewModel>>(buyersItems);
            ICollection<CompanyViewModel> sellersViewModel = this.Mapper.Map<ICollection<CompanyViewModel>>(sellerItems);

            return View(buyersViewModel.Concat(sellersViewModel).ToList());
        }

        [HasPermission(Permissions.CompanyDelete)]
        public ActionResult Delete(Guid id)
        {
            return View(this.Mapper.Map<CompanyViewModel>(this._companyManager.Get(id)));
        }

        [HttpPost]
        [HasPermission(Permissions.CompanyDelete)]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            CompanyViewModel company = this.Mapper.Map<CompanyViewModel>(this._companyManager.Get(id));
            await this._companyManager.Delete(id);
            
            return View("DeleteOk", company);
        }
    }
}