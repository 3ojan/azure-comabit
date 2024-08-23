// <copyright file="InquiryServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Inquiry;
using Comabit.DL.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Test
{
    public class InquiryServiceTests : BaseServiceTests
    {
        private InquiryService _inquiryService;
        private CompanyService _companyService;
        private ElasticSearchService _elasticSearchService;

        private PortfolioService _portfolioService;

        [SetUp]
        public void Setup()
        {
            this._inquiryService = new InquiryService(this.UnitOfWork);
            this._companyService = new CompanyService(this.UnitOfWork);
            this._portfolioService = new PortfolioService(this.UnitOfWork);
            this._elasticSearchService = new ElasticSearchService();
        }

        [Test]
        public void GetAllBuyerProjectsTest()
        {
            var result = this._inquiryService.GetAllBuyerProjects().ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetBuyerProjectsByCompanyIdTest()
        {
            var result = this._inquiryService.GetBuyerProjectsByBuyerCompanyId(new Guid("91cf7604-95eb-4691-b701-ab437a6e003a")).ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetBuyerProjectsInquiryByIdTest()
        {
            var result = this._inquiryService.GetBuyerProjectInquiryById(new Guid("91cf7604-95eb-4691-b701-ab437a6e003a")).ToList();

            Assert.IsNotNull(result);
        }
    }
}