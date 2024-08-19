// <copyright file="SettingController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Seller.Controllers
{
    using Comabit.BL.Company;
    using Comabit.BL.Company.Dto;
    using Comabit.BL.Geo;
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Identity;
    using Comabit.BL.Porfolio;
    using Comabit.BL.Porfolio.Dto;
    using Comabit.DL;
    using Comabit.DL.Interfaces;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Admin.Models;
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using Comabit.UI.Models.Portfolio;
    using Comabit.UI.Areas.Seller.Models.Settings;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;
    using Comabit.UI.Models.Match;
    using Comabit.UI.Models.Company;

    [Area("Seller")]
    public class SettingController : BaseController
    {
        public readonly PortfolioManager _portfolioManager;

        public readonly CompanyManager _companyManager;

        public readonly SellerCompanyManager _sellerCompanyManager;

        public readonly UserManager _userManager;

        public readonly GeoManager _geoManager;

        public SettingController(GeoManager geoManager, UserManager userManager, CompanyManager companyManager, SellerCompanyManager sellerCompanyManager, PortfolioManager portfolioManager, ILogger<HomeController> logger) : base(logger)
        {
            this._portfolioManager = portfolioManager;
            this._companyManager = companyManager;
            this._sellerCompanyManager = sellerCompanyManager;
            this._userManager = userManager;
            this._geoManager = geoManager;
        }

        public async Task<ActionResult> EditPortfolio()
        {
            if (User.HasCompany())
            {
                Guid companyId = User.GetCompanyId();

                ICollection<AreaViewModel> portfolioAreas = this.Mapper.Map<ICollection<AreaViewModel>>(await this._sellerCompanyManager.GetPortfolioForEdit(companyId));

                return View(new PortfolioEditViewModel(companyId, portfolioAreas));
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> EditPortfolio(PortfolioEditViewModel editViewModel)
        {
            ICollection<PortfolioAreaItem> portfolios = this.Mapper.Map<ICollection<PortfolioAreaItem>>(editViewModel.PortfolioAreas);

            await this._sellerCompanyManager.UpdatePortfolio(editViewModel.CompanyId, portfolios);

            return View("EditPortfolioOk");
        }

        public async Task<ActionResult> EditCommunities()
        {
            if (User.HasCompany())
            {
                Guid companyId = User.GetCompanyId();

                CommunitiesEditViewModel viewModel = new CommunitiesEditViewModel()
                {
                    Communities = this.Mapper.Map<ICollection<CommunityViewModel>>(await _sellerCompanyManager.GetCommunitiesForEdit(companyId)),
                    CompanyId = companyId,
                    States = this.Mapper.Map<ICollection<StateViewModel>>(await _geoManager.GetStates())
                };

                return View(viewModel);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> EditCommunities(CommunitiesEditViewModel viewModel)
        {
            ICollection<CommunityItem> communities = this.Mapper.Map<ICollection<CommunityItem>>(viewModel.Communities);

            await this._sellerCompanyManager.UpdateCommunities(viewModel.CompanyId, communities);

            return View("EditCommunitiesOk");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonNetResult> AddCommunity(CommunitiesEditViewModel viewModel)
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
        public async Task<JsonNetResult> RemoveCommunity(CommunitiesEditViewModel viewModel)
        {
            viewModel.Communities.Remove(viewModel.Communities.Where(c => c.Id == viewModel.NewCommunityId).FirstOrDefault());

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_SellerCommunitiesEditor", viewModel),
            });
        }
    }

}