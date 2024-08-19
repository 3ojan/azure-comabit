// <copyright file="ElasticSearchManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Company.Dto;
using Comabit.BL.ElasticSearch;
using Comabit.BL.Geo;
using Comabit.BL.Geo.Dto;
using Comabit.BL.Inquiry.Dto;
using Comabit.BL.Match;
using Comabit.BL.Match.Dto;
using Comabit.BL.Porfolio;
using Comabit.BL.Porfolio.Dto;
using Comabit.BL.Tax.Dto;
using Comabit.DL.Data.Company;
using Comabit.DL.Interfaces;
using Comabit.DL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.BL.Test
{
    public class ElasticSearchManagerTests : BaseManagerTests
    {
        private ElasticSearchService _elasticSearchService;

        private ElasticSearchManager _elasticSearchManager;

        private ICompanyService _companyService;

        private IMatchService _matchService;

        private MatchManager _matchManager;

        private IInquiryService _inquiryService;

        private ILogService _logService;

        [SetUp]
        public void Setup()
        {
            this._companyService = new CompanyService(this.UnitOfWork);
            this._matchService = new MatchService(this.UnitOfWork);
            this._inquiryService = new InquiryService(this.UnitOfWork);
            this._logService = new LogService(this.UnitOfWork);
            this._elasticSearchService = new ElasticSearchService();
            this._matchManager = new MatchManager(this._matchService, this._companyService, this._elasticSearchService, this._logService);
            this._elasticSearchManager = new ElasticSearchManager(this._logService, this._companyService, new ElasticSearchService(), this._matchService);
        }

        private InquiryItem GetTestInquiry()
        {
            return new InquiryItem()
            {
                Id = new Guid(),
                DeliveryPostalCode = "012345",
                PortfolioCategories = new List<PortfolioCategoryItem>()
                {
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000002") },
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000003") }
                },
                PortfolioSubCategories = new List<PortfolioSubCategoryItem>()
                {
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000002") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000003") }
                },
                AddidtionalTags = "Eins, Zwei, Drei"
            };
        }

        private SellerItem GetTestSeller()
        {
            return new SellerItem()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000010"),
                Name = "Seller 1",
                PostalCode = "012345",
                PortfolioCategories = new List<PortfolioCategoryItem>()
                {
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000002") },
                    new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000003") }
                },
                PortfolioSubCategories = new List<PortfolioSubCategoryItem>()
                {
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000002") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000003") },
                    new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000004") }
                },
                AdditionalPortfolioCategoryTags = new List<AdditionalPortfolioCategoryTagsItem>()
                {
                    new AdditionalPortfolioCategoryTagsItem()
                    {
                        Id = new Guid(),
                        Tags = "Eins, Zwei",
                        PortfolioCategoryId = new Guid("00000000-0000-0000-0000-000000000000"),
                        SellerCompanyId = new Guid("00000000-0000-0000-0000-000000000010")
                    }
                },
                Communities = new List<CommunityItem>()
                {
                   new CommunityItem()
                   {
                       Name = "Testcommunity", Cities = new List<CityItem>()
                       {
                           new CityItem() { PostalCode = "012345", Name = "City1" },
                           new CityItem() { PostalCode = "012346", Name = "City2" }
                       }
                   }
                }
            };
        }

        [Test, Order(1)]
        public async ValueTask AddAllSellersToIndexTest()
        {
            var sellers = this._companyService.GetAllSellers().ToList();

            var result = true;

            foreach (var seller in sellers)
            {
                result = result && await this._elasticSearchManager.AddSeller(seller.Id);
            }

            Assert.IsTrue(result);
        }

        [Test, Order(2)]
        public async ValueTask SearchMatchesForInquiryTest()
        {
            var projects = this._inquiryService.GetAllBuyerProjects()
                .ToList();

            List<MatchItem> matches = new();

            foreach (var project in projects)
            {
                foreach (var inquiry in project.Inquiries)
                {
                    matches.AddRange(await this._elasticSearchManager.SearchMatchesForInquiry(this.Mapper.Map<InquiryItem>(inquiry)));
                }
            }

            Assert.IsTrue(matches.Any());
        }

        [Test, Order(3)]
        public async ValueTask CreateMatchesForInquiryTest()
        {
            var projects = this._inquiryService.GetAllBuyerProjects().ToList();

            var counter = 0;

            foreach (var project in projects)
            {
                foreach (var inquiry in project.Inquiries)
                {
                    if (await this._matchManager.CreateMatchesForInquiry(this.Mapper.Map<InquiryItem>(inquiry)))
                    {
                        counter++;
                    }
                }
            }

            Assert.Greater(counter, 0);
        }

        [Test, Order(4)]
        public async ValueTask UpdateAllSellersInIndexTest()
        {
            var sellers = this._companyService.GetAllSellers().ToList();

            var result = true;

            foreach (var seller in sellers)
            {
                result = result && await this._elasticSearchManager.UpdateSeller(seller.Id);
            }

            Assert.IsTrue(result);
        }

        [Test, Order(5)]
        public async ValueTask CheckSellersCountSyncedToActiveSellersTest()
        {
            var dbSellersCount = this._companyService.GetAllSellers().Count();

            var indexSellersCount = await this._elasticSearchManager.GetAllSellersCount();

            Assert.IsTrue(dbSellersCount == indexSellersCount);
        }

        [Test, Order(6)]
        public async ValueTask DeleteAllSellersFromIndexTest()
        {
            var sellers = this._companyService.GetAllSellers().ToList();

            var result = true;

            foreach (var seller in sellers)
            {
                result = result && await this._elasticSearchManager.DeleteSeller(seller.Id);
            }

            var addResult = true;

            foreach (var seller in sellers)
            {
                addResult = addResult && await this._elasticSearchManager.AddSeller(seller.Id);
            }

            Assert.IsTrue(result && addResult);
        }

        [Test, Order(7)]
        public async ValueTask TestMatchingSellerAndInquiry()
        {
            var seller = GetTestSeller();
            var inquiry = GetTestInquiry();

            bool added = await this._elasticSearchManager.AddSeller(this.Mapper.Map<Seller>(seller));

            inquiry.DeliveryPostalCode = "012345";

            List<MatchItem> matches = await this._elasticSearchManager.SearchMatchesForInquiry(inquiry);

            bool deleted = await this._elasticSearchManager.DeleteSeller(seller.Id);

            Assert.IsTrue(matches.Count > 0);
        }

        [Test, Order(7)]
        public async ValueTask TestPostalCodeWithNoMatchingSeller()
        {
            var seller = GetTestSeller();
            var inquiry = GetTestInquiry();

            bool added = await this._elasticSearchManager.AddSeller(this.Mapper.Map<Seller>(seller));

            inquiry.DeliveryPostalCode = "000000";

            List<MatchItem> matches = await this._elasticSearchManager.SearchMatchesForInquiry(inquiry);

            bool deleted = await this._elasticSearchManager.DeleteSeller(seller.Id);

            Assert.IsTrue(matches.Count == 0);
        }

        [Test, Order(7)]
        public async ValueTask TestSellersWithoutMatchingSubcategory()
        {
            var seller1 = GetTestSeller();
            var seller2 = GetTestSeller();
            var seller3 = GetTestSeller();
            var inquiry = GetTestInquiry();

            seller2.Id = new Guid("00000000-0000-0000-0000-000000000020");
            seller2.Name = "Seller 2";
            seller2.PortfolioCategories = new List<PortfolioCategoryItem>()
            {
                new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
            };
            seller2.PortfolioSubCategories = new List<PortfolioSubCategoryItem>()
            {
                new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
            };

            seller3.Id = new Guid("00000000-0000-0000-0000-000000000030");
            seller3.Name = "Seller 3";
            seller3.PortfolioCategories = new List<PortfolioCategoryItem>()
            {
                new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
                new PortfolioCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000002") },
            };
            seller3.PortfolioSubCategories = new List<PortfolioSubCategoryItem>()
            {
                new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000000") },
                new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000001") },
                new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000002") },
            };

            inquiry.PortfolioSubCategories = new List<PortfolioSubCategoryItem>()
            {
                new PortfolioSubCategoryItem() { Id = new Guid("00000000-0000-0000-0000-000000000abc") }
            };

            bool added = await this._elasticSearchManager.AddSeller(this.Mapper.Map<Seller>(seller1));
            added = await this._elasticSearchManager.AddSeller(this.Mapper.Map<Seller>(seller2));
            added = await this._elasticSearchManager.AddSeller(this.Mapper.Map<Seller>(seller3));

            List<MatchItem> matches = await this._elasticSearchManager.SearchMatchesForInquiry(inquiry, minMatchPercentageForSubCategories: 0);

            bool deleted = await this._elasticSearchManager.DeleteSeller(seller1.Id);
            deleted = await this._elasticSearchManager.DeleteSeller(seller2.Id);
            deleted = await this._elasticSearchManager.DeleteSeller(seller3.Id);

            Assert.IsTrue(matches.Count > 0);
        }
    }
}