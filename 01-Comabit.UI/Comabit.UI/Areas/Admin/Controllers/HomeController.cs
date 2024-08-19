// <copyright file="HomeController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Admin.Controllers
{
    using Comabit.BL.Company;
    using Comabit.BL.Geo;
    using Comabit.BL.Inquiry;
    using Comabit.BL.Log;
    using Comabit.DL.Data.Inquiry;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Admin.Models.Home;
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using Comabit.UI.Areas.Shared;
    using Comabit.UI.Areas.Shared.Models;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class HomeController : BaseController
    {
        private readonly GeoManager _geoManager;
        private readonly CompanyManager _companyManager;
        private readonly LogManager _logManager;

        public HomeController(LogManager logManager, CompanyManager companyManager, GeoManager geoManager, ILogger<CompanyController> logger) : base(logger)
        {
            this._companyManager = companyManager;
            this._geoManager = geoManager;
            this._logManager = logManager;
        }

        public async Task<ActionResult> Index()
        {
            ICollection<LogViewModel> logs = this.Mapper.Map<ICollection<LogViewModel>>(await this._logManager.Get(amount: 10));
            var counterSellers = await this._companyManager.GetCountSellers(7, 14);
            var counterBuyers = await this._companyManager.GetCountBuyers(7, 14);
            var buyerSellerTrend = await this.RetrieveBuyerSellerTrend();

            var model = new AdminIndexViewModel()
            {
                Logs = logs,
                CountSellers = counterSellers.CountOverAll,
                NewSellersSinceLastWeekPercent = Factory.RetrieveTrend(counterSellers),
                CountBuyers = counterBuyers.CountOverAll,
                NewBuyersSinceLastWeekPercent = Factory.RetrieveTrend(counterBuyers),
                BuyerSellerTrendJson = JsonConvert.SerializeObject(buyerSellerTrend)
            };

            return View(model);
        }

        private async Task<ChartViewModel> RetrieveBuyerSellerTrend()
        {
            var toDate = DateTime.Now;
            var fromDate = toDate.AddMonths(-11);
            var buyerCounts = await this._companyManager.GetCountBuyerByMonth(fromDate, toDate);
            var sellerCounts = await this._companyManager.GetCountSellerByMonth(fromDate, toDate);
            ChartViewModel result = Factory.MapDateCount2ListsResultToChartViewModel(buyerCounts, sellerCounts);
            return result;
        }
    }
}