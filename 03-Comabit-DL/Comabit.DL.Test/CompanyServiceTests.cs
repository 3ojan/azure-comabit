// <copyright file="CompanyServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Company;
using Comabit.DL.Data.Portfolio;
using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.DL.Test
{
    public class CompanyServiceTests : BaseServiceTests
    {
        private CompanyService _companyService;
        private PortfolioService _portfolioService;
        private Guid _sellerCompanyId = new Guid("311e53fa-4a45-44ae-aa07-39b77ccdfc90");
        private Guid _buyerCompanyId = new Guid("91cf7604-95eb-4691-b701-ab437a6e003a");

        [SetUp]
        public void Setup()
        {
            this._companyService = new CompanyService(this.UnitOfWork);
            this._portfolioService = new PortfolioService(this.UnitOfWork);
        }

        [Test]
        public void GetAllCompanies()
        {
            var result = this._companyService.GetAllCompanies().ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetBuyerCompanyTest()
        {
            var result = this._companyService.GetBuyerById(this._buyerCompanyId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddBuyerCompanyTest()
        {
            var input = new Buyer()
            {
                Id = Guid.NewGuid(),
                Name = "Test - Mko (Buyer)",
                PostalCode = "89231",
                City = "Neu-Ulm",
                UstId = "TestUstId",
                Confirmed = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            this._companyService.AddBuyer(input);
            await this._companyService.SaveAsync();
        }

        [Test]
        public async Task UpdateBuyerCompany()
        {
            var buyerCompany = this._companyService.GetBuyerById(this._buyerCompanyId);

            buyerCompany.UpdatedAt = DateTime.Now;

            this._companyService.UpdateBuyer(buyerCompany);
            await this._companyService.SaveAsync();
        }

        [Test]
        public void GetSellerCompanyTest()
        {
            var result = this._companyService.GetSellerCompany(this._sellerCompanyId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddSellerCompanyTest()
        {
            var input = new Seller()
            {
                Id = Guid.NewGuid(),
                Name = "Test - Mko (Seller)",
                PostalCode = "89231",
                City = "Neu-Ulm",
                UstId = "TestUstId",
                Confirmed = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            this._companyService.AddSeller(input);
            await this._companyService.SaveAsync();
        }

        [Test]
        public async Task UpdateSellerCompanyTest()
        {
            var sellerCompany = this._companyService.GetSellerCompany(this._sellerCompanyId).Single();

            //var additionalTag = sellerCompany.AdditionalPortfolioCategoryTags.First();

            //sellerCompany.AdditionalPortfolioCategoryTags.Remove(additionalTag);
            sellerCompany.UpdatedAt = DateTime.Now;

            this._companyService.UpdateSeller(sellerCompany);
            await this._companyService.SaveAsync();
        }

        [Test]
        public async Task UpdateSellerCompanyWithCategoryTest2()
        {
            var sellerCompany = this._companyService.GetSellerCompany(this._sellerCompanyId).First();

            var allCategories = this._portfolioService.GetAllCategoriesByAreaId(new Guid("29c12b1d-70ed-2d44-0b5e-eb6a7d9beef6")).ToList();

            foreach (var item in allCategories)
            {
                sellerCompany.PortfolioCategories.Add(item);
            }

            this._companyService.UpdateSeller(sellerCompany);
            await this._companyService.SaveAsync();
        }

        [Test]
        public async Task UpdateSellerCompanyWithSubCategoryTest()
        {
            var sellerCompany = this._companyService.GetSellerCompany(this._sellerCompanyId).Single();

            var allSubCategories = this._portfolioService.GetSubCategoriesByCategoryId(new Guid("b8e4b213-c747-c3dd-33b4-2bb37e7b9da1")).ToList();

            ICollection<PortfolioSubCategory> subcat = new List<PortfolioSubCategory>();

            foreach (var item in allSubCategories)
            {
                subcat.Add(item);
                //sellerCompany.PortfolioSubCategories.Add(item);
            }

            sellerCompany.PortfolioSubCategories = subcat;

            this._companyService.UpdateSeller(sellerCompany);
            await this._companyService.SaveAsync();
        }

        [Test]
        public async Task AddAdditionalCategoryTagsToSellerCompanyTest()
        {
            var sellerCompany = this._companyService.GetSellerCompany(this._sellerCompanyId).Single();

            var allCategories = this._portfolioService.GetAllCategoriesByAreaId(new Guid("29c12b1d-70ed-2d44-0b5e-eb6a7d9beef6")).ToList();

            var category = allCategories.Where(c => c.Id == new Guid("2fba8d3d-fa34-b4a8-8276-a3ea7586d180")).Single();

            var additionalTags = new AdditionalPortfolioCategoryTags()
            {
                Id = Guid.NewGuid(),
                Tags = "Test,Test2",
                PortfolioCategoryId = category.Id,
                SellerCompanyId = sellerCompany.Id
            };

            this._portfolioService.AddAdditionalPortfolioCategoryTags(additionalTags);
            await this._portfolioService.SaveAsync();
        }

        [Test]
        public async Task UpdateSellerCompanyWithAdditionalTagsTest()
        {
            //// additionalTags gehen so nicht
            var sellerCompany = this._companyService.GetSellerCompany(this._sellerCompanyId).Single();

            var allCategories = this._portfolioService.GetAllCategoriesByAreaId(new Guid("29c12b1d-70ed-2d44-0b5e-eb6a7d9beef6")).ToList();

            var category = allCategories.Where(c => c.Id == new Guid("29c12b1d-70ed-2d44-0b5e-eb6a7d9beef6")).Single();

            ICollection<AdditionalPortfolioCategoryTags> tags = new List<AdditionalPortfolioCategoryTags>();

            var additionalTags = new AdditionalPortfolioCategoryTags()
            {
                Id = Guid.NewGuid(),
                Tags = "Test,Test2",
                PortfolioCategoryId = category.Id
            };

            tags.Add(additionalTags);

            sellerCompany.AdditionalPortfolioCategoryTags = tags;

            this._companyService.UpdateSeller(sellerCompany);
            await this._companyService.SaveAsync();
        }
    }
}