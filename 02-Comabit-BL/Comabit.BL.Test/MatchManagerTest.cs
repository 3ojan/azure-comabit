// <copyright file="SellerCompanyManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Company;
using Comabit.BL.Match;
using Comabit.DL.Interfaces;
using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.BL.Test
{
    public class MatchManagerTest : BaseManagerTests
    {
        private MatchManager _matchManager;

        private ElasticSearchService _elasticSearchService;

        private ICompanyService _companyService;

        private IMatchService _matchService;

        private IInquiryService _inquiryService;

        private ILogService _logService;

        private Guid SellerId = new Guid("9cb9deb4-c2d1-4846-9907-a5fbbf0083c5");

        [SetUp]
        public void Setup()
        {
            this._companyService = new CompanyService(this.UnitOfWork);
            this._matchService = new MatchService(this.UnitOfWork);
            this._inquiryService = new InquiryService(this.UnitOfWork);
            this._logService = new LogService(this.UnitOfWork);
            this._elasticSearchService = new ElasticSearchService();
            this._matchManager = new MatchManager(this._matchService, this._companyService, this._elasticSearchService, this._logService);
        }
        
        [Test]
        public async ValueTask GetCountSellerMatchesByMonthTestAsync()
        {
            var toDate = DateTime.Now;
            var fromDate = toDate.AddMonths(-11);
            var result = await this._matchManager.GetCountSellerMatchesByMonth(SellerId, fromDate, toDate, null);

            Assert.IsNotNull(result);
        }
    }
}