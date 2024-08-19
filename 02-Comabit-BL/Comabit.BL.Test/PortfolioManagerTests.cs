// <copyright file="PortfolioManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Porfolio;
using Comabit.DL.DBDal.Services;
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
    public class PortfolioManagerTests : BaseManagerTests
    {
        private IPortfolioService _portfolioService;
        private PortfolioManager _portfolioManager;

        [SetUp]
        public void Setup()
        {
            this._portfolioService = new PortfolioService(this.UnitOfWork);
            this._portfolioManager = new PortfolioManager(this._portfolioService);
        }

        [Test]
        public async ValueTask RetrievePortfolioAreasTestAsync()
        {
            var result = await this._portfolioManager.RetrievePortfolioAreas();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public async ValueTask RetrievePortfolioCategoriesTest()
        {
            var result = await this._portfolioManager.RetrievePortfolioCategories(new Guid("29c12b1d-70ed-2d44-0b5e-eb6a7d9beef6"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public async ValueTask RetrievePortfolioSubCategoriesTest()
        {
            var result = await this._portfolioManager.RetrievePortfolioSubCategories(new Guid("29b93372-72b3-8d1a-8799-24ae6b5e93f0"));

            Assert.IsNotNull(result);
        }
    }
}