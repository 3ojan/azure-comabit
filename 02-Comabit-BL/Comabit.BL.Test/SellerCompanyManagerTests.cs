// <copyright file="SellerCompanyManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Company;
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
    public class SellerCompanyManagerTests : BaseManagerTests
    {
        private IPortfolioService _portfolioService;
        private ICompanyService _companyService;
        private IGeoService _geoService;
        private SellerCompanyManager _sellerCompanyManager;
        private IMatchService _matchservice;
        private ILogService _logService;
        private Guid _sellerCompanyId = new Guid("06c0770b-8601-4b34-bc13-55de1a52d9dd");

        [SetUp]
        public void Setup()
        {
            this._portfolioService = new PortfolioService(this.UnitOfWork);
            this._companyService = new CompanyService(this.UnitOfWork);
            this._geoService = new GeoService(this.UnitOfWork);
            this._matchservice = new MatchService(this.UnitOfWork);
            this._sellerCompanyManager = new SellerCompanyManager(this._logService, this._geoService, this._portfolioService, this._companyService, this._matchservice, new ElasticSearchService());
        }

        [Test]
        public async ValueTask GetSellerCompanyTestAsync()
        {
            var result = await this._sellerCompanyManager.GetSellerCompany(this._sellerCompanyId);

            Assert.IsNotNull(result);
        }
    }
}