// <copyright file="ElasticSearchService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Inquiry;
using Comabit.DL.Data.ElasticSearch;
using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comabit.DL.Interfaces;
using Comabit.DL.Data.Portfolio;

namespace Comabit.DL.Test
{
    public class ElasticSearchServiceTests : BaseServiceTests
    {
        private IElasticSearchService _elasticSearchService;

        private ICompanyService _companyService;

        [SetUp]
        public void Setup()
        {
            this._companyService = new CompanyService(this.UnitOfWork);
            this._elasticSearchService = new ElasticSearchService();
        }

        [Test, Order(1)]
        public async ValueTask AddSellerToIndexTest()
        {
            var seller = new SellerDoc() 
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1"),
                Name = "Verkäufer A",
                Categories = new List<CategoryDoc>() 
                { 
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaac1"), Name = "Kategorie A", Tags = "Lorem, Ipsum, Dolor, Sit, Amet" },
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaac2"), Name = "Kategorie B", Tags = "Lorem, Ipsam, Dolam" }
                },
                SubCategories = new List<CategoryDoc>()
                {
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaad1"), Name = "Unterkategorie X", Tags = "SubLoram, SubIpsum, SubDolor, SubSit, SubAmet" },
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaad2"), Name = "Unterkategorie Y", Tags = "SubLorem, SubIpsum, SubDolor" }
                }, 
                Cities = new List<CityDoc>()
                {
                    new CityDoc() { CommunityName = "Stadtkreis Ulm", Name = "Ulm", Location = "48.4015,9.9927", PostalCode = "89073" },
                    new CityDoc() { CommunityName = "Stadtkreis Ulm", Name = "Ulm", Location = "48.4171,9.9635", PostalCode = "89075" },
                    new CityDoc() { CommunityName = "Bodenseekreis", Name = "Stetten", Location = "47.6901,9.2986", PostalCode = "88719" }
                }
            };

            seller.CategoryIds = seller.Categories.Select(c => c.Id).ToList();
            seller.SubCategoryIds = seller.SubCategories.Select(c => c.Id).ToList();

            var result = await this._elasticSearchService.AddSeller(seller);

            seller = new SellerDoc()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab2"),
                Name = "Verkäufer B",
                Categories = new List<CategoryDoc>()
                {
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaac1"), Name = "Kategorie A", Tags = "Lorem, Amet" },
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaac3"), Name = "Kategorie C", Tags = "Lorem, Dolom" }
                },
                SubCategories = new List<CategoryDoc>()
                {
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaad1"), Name = "Unterkategorie X", Tags = "SubLoram, SubIpsim, SubDolur, SubSat, SubAmit" },
                    new CategoryDoc() { Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaad3"), Name = "Unterkategorie Z", Tags = "SubLorem, SubIpsum, SubDolor" }
                },
                Cities = new List<CityDoc>()
                {
                    new CityDoc() { CommunityName = "Bodenseekreis", Name = "Markdorf", Location = "47.7192,9.3903", PostalCode = "88677" },
                    new CityDoc() { CommunityName = "Landkreis Biberach", Name = "Unlingen", Location = "48.1673,9.5222", PostalCode = "88527" }
                }
            };

            seller.CategoryIds = seller.Categories.Select(c => c.Id).ToList();
            seller.SubCategoryIds = seller.SubCategories.Select(c => c.Id).ToList();

            result = result && await this._elasticSearchService.AddSeller(seller);

            Assert.IsTrue(result);
        }

        [Test, Order(2)]
        public async ValueTask FindSellerTest()
        {
            Inquiry inquiry = new Inquiry()
            {
                Project = new Project()
                {
                    PostalCode = "89075"
                },
                PortfolioCategories = new List<PortfolioCategory>()
                {
                    new PortfolioCategory()
                    {
                         Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaac1")
                    },
                    new PortfolioCategory()
                    {
                         Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaac1")
                    }
                },
                PortfolioSubCategories = new List<PortfolioSubCategory>()
                {
                    new PortfolioSubCategory()
                    {
                         Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaad1")
                    }
                }
            };

            var result = await this._elasticSearchService.GetMatchingSellersForInquiry(inquiry);

            Assert.IsTrue(result.Any());
        }

        [Test, Order(3)]
        public async ValueTask UpdateSellerTest()
        {
            var seller = new SellerDoc()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1"),
                Name = "Verkäufer A geändert"
            };

            var result = await this._elasticSearchService.UpdateSeller(seller);

            Assert.IsTrue(result);
        }

        [Test, Order(4)]
        public async ValueTask DeleteSellerTest()
        {
            var seller = new SellerDoc()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1")
            };

            var result = await this._elasticSearchService.DeleteSeller(seller);

            seller = new SellerDoc()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab2")
            };

            result = result && await this._elasticSearchService.DeleteSeller(seller);

            Assert.IsTrue(result);
        }
    }
}