// <copyright file="PortfolioServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace Comabit.DL.Test
{
    public class PortfolioServiceTests : BaseServiceTests
    {
        private PortfolioService _portfolioService;

        [SetUp]
        public void Setup()
        {
            this._portfolioService = new PortfolioService(this.UnitOfWork);
        }

        [Test]
        public void GetAllAreasTese()
        {
            var result = this._portfolioService.GetAllAreas().ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllCategoriesTest()
        {
            var result = this._portfolioService.GetAllCategories().ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllCategoriesByAreaIdTest()
        {
            var areaId = new Guid("29c12b1d-70ed-2d44-0b5e-eb6a7d9beef6");
            var result = this._portfolioService.GetAllCategoriesByAreaId(areaId).ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetSubCategoriesTest()
        {
            var areaCategoryId = new Guid("29b93372-72b3-8d1a-8799-24ae6b5e93f0");
            var result = this._portfolioService.GetSubCategoriesByCategoryId(areaCategoryId).ToList();

            Assert.IsNotNull(result);
        }
    }
}