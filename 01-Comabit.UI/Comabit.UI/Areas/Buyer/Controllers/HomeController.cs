// <copyright file="HomeController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Buyer.Controllers
{
    using Comabit.BL.Inquiry;
    using Comabit.BL.Match;
    using Comabit.BL.Porfolio;
    using Comabit.BL.Shared.DTO;
    using Comabit.DL.Data.Match;
    using Comabit.UI.Areas.Buyer.Models.Home;
    using Comabit.UI.Areas.Shared;
    using Comabit.UI.Areas.Shared.Models;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area(Roles.Buyer)]
    public class HomeController : BaseController
    {
        private PortfolioManager _portfolioManager;
        private MatchManager _matchManager;
        private InquiryManager _inquiryManager;

        public HomeController(PortfolioManager portfolioManager, MatchManager matchManager, InquiryManager inquiryManager, ILogger<HomeController> logger) : base(logger)
        {
            this._portfolioManager = portfolioManager;
            this._matchManager = matchManager;
            this._inquiryManager = inquiryManager;
        }

        public async Task<ActionResult> Index()
        {
            var BuyerId = User.GetCompanyId();
            var offerTrend = await this.RetrieveOfferTrend(BuyerId);
            var newOffers = await this._matchManager.GetCountBuyerOffersByDays(BuyerId, null, 7, 14);
            var orderedOffers = await this._matchManager.GetCountBuyerOffersByDays(BuyerId, new List<OfferState>() { OfferState.ordered }, 7, 14);
            var offers = this.Mapper.Map<ICollection<OfferViewModel>>(await this._matchManager.GetOffersForBuyerWithLimit(User.GetCompanyId(), 5));
            var inquiriesCreated = this.Mapper.Map<ICollection<InquiryViewModel>>(await this._inquiryManager.GetInquiries(User.GetCompanyId(), 4, false));
            var inquiriesUpdated = this.Mapper.Map<ICollection<InquiryViewModel>>(await this._inquiryManager.GetInquiries(User.GetCompanyId(), 4, true));
            var model = new BuyerHomeIndexViewModel()
            {
                CountNewOffers = newOffers.CountValue1,
                NewOffersSinceLastWeekPercent = Factory.RetrieveTrend(newOffers),
                CountOrderedMatches = orderedOffers.CountValue1,
                OrderedMatchesSinceLastWeekPercent = Factory.RetrieveTrend(orderedOffers),
                OffersTrendJson = JsonConvert.SerializeObject(offerTrend),
                InquiriesOrderByCreated = inquiriesCreated,
                InquiriesOrderByUpdated = inquiriesUpdated,
                Offers = offers
            };

            return View(model);
        }

        private async Task<ChartViewModel> RetrieveOfferTrend(Guid userid)
        {
            var toDate = DateTime.Now;
            var fromDate = toDate.AddMonths(-11);
            var offerCounts = await this._matchManager.GetCountBuyerOffersByMonth(userid, fromDate, toDate, null);
            ChartViewModel result = Factory.MapDateCountResultToChartViewModel(offerCounts);

            return result;
        }
    }
}