// <copyright file="HomeController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Seller.Controllers
{
    using Comabit.BL.Match;
    using Comabit.BL.Shared.DTO;
    using Comabit.DL;
    using Comabit.DL.Data.Match;
    using Comabit.DL.Interfaces;
    using Comabit.UI.Areas.Seller.Models.Home;
    using Comabit.UI.Areas.Shared;
    using Comabit.UI.Areas.Shared.Models;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Seller")]
    public class HomeController : BaseController
    {
        private MatchManager _matchManager;

        public HomeController(MatchManager matchManager, ILogger<HomeController> logger) : base(logger)
        {
            this._matchManager = matchManager;
        }

        public async Task<ActionResult> Index()
        {
            var SellerId = User.GetCompanyId();
            var inquiriesTrend = await this.RetrieveInquiriesTrend(SellerId);
            var commissonByMonth = await this.RetrieveCommissionByMonth(SellerId);
            var newInquiries = await this._matchManager.GetCountSellerMatchesByDays(SellerId, null, 7, 14);
            var openOffers = await this._matchManager.GetCountSellerMatchesByDays(SellerId, new List<MatchState>() { MatchState.offered }, 7, 14);
            var commissions = await this._matchManager.GetCountSellerMatchesByDays(SellerId, new List<MatchState>() { MatchState.ordered }, 7, 14);
            var Matches = this.Mapper.Map<ICollection<MatchViewModel>>(await this._matchManager.GetSellerMatches(User.GetCompanyId(), new List<MatchState>() { MatchState.pending, MatchState.revoked }, 4));
            var model = new SellerHomeIndexViewModel()
            {
                CountNewInquiries = newInquiries.CountValue1,
                NewInquiriesSinceLastWeekPercent = Factory.RetrieveTrend(newInquiries),
                CountOpenOffers = openOffers.CountValue1,
                OpenOffersSinceLastWeekPercent = Factory.RetrieveTrend(newInquiries),
                CountCommissions = commissions.CountValue1,
                CommissionsSinceLastWeekPercent = Factory.RetrieveTrend(commissions),
                InquiriesTrendJson = JsonConvert.SerializeObject(inquiriesTrend),
                CommissionByMonthJson = JsonConvert.SerializeObject(commissonByMonth),
                Matches = Matches
            };
            return View(model);
        }

        private async Task<ChartViewModel> RetrieveCommissionByMonth(Guid userid)
        {
            var toDate = DateTime.Now;
            var fromDate = toDate.AddMonths(-11);
            var commissionCounts = await this._matchManager.GetCountSellerMatchesByMonth(userid, fromDate, toDate, new List<MatchState>() { MatchState.ordered });
            ChartViewModel result = Factory.MapDateCountResultToChartViewModel(commissionCounts);
            return result;
        }

        private async Task<ChartViewModel> RetrieveInquiriesTrend(Guid userid)
        {
            var toDate = DateTime.Now;
            var fromDate = toDate.AddMonths(-11);
            var inquiriesCounts = await this._matchManager.GetCountSellerMatchesByMonth(userid, fromDate, toDate, null);
            ChartViewModel result = Factory.MapDateCountResultToChartViewModel(inquiriesCounts);
            return result;
        }

    }
}