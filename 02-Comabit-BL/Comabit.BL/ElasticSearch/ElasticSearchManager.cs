// <copyright file="ElasticSearchManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.ElasticSearch
{
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL.Data.Match;
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Comabit.DL.Data.ElasticSearch;
    using Comabit.DL.Data.Company;
    using System.Globalization;
    using Comabit.DL.Data.Inquiry;
    using Comabit.BL.Match.Dto;
    using Comabit.BL.Company.Dto;
    using Comabit.BL.Inquiry.Dto;

    public class ElasticSearchManager : BaseManager
    {
        private ICompanyService _companyService;

        private IElasticSearchService _elasticSearchService;

        private IMatchService _matchservice;

        private ILogService _logService;

        public ElasticSearchManager(ILogService logService, ICompanyService companyService, IElasticSearchService elasticSearchService, IMatchService matchservice)
        {
            this._companyService = companyService;
            this._elasticSearchService = elasticSearchService;
            this._matchservice = matchservice;
            this._logService = logService;
        }

        /// <summary>
        /// count all sellers in index
        /// </summary>
        public async Task<long> GetAllSellersCount()
        {
            var sellersCount = await this._elasticSearchService.GetAllSellersCount();

            return sellersCount;
        }

        /// <summary>
        /// get seller by id and add to search index
        /// </summary>
        /// <param name="id">seller id</param>
        public async Task<bool> AddSeller(Guid id)
        {
            Seller seller = GetSeller(id);

            return await this.AddSeller(seller);
        }

        /// <summary>
        /// add seller object to search index
        /// </summary>
        /// <param name="seller">seller</param>
        public async Task<bool> AddSeller(Seller seller)
        {
            var sellerDoc = this.GetSellerDoc(seller);

            return await this._elasticSearchService.AddSeller(sellerDoc);
        }

        /// <summary>
        /// get seller by id and update in search index
        /// </summary>
        /// <param name="id">seller id</param>
        public async Task<bool> UpdateSeller(Guid id)
        {
            var seller = GetSeller(id);

            return await this.UpdateSeller(seller);
        }

        /// <summary>
        /// update seller in search index
        /// </summary>
        /// <param name="id">seller id</param>
        public async Task<bool> UpdateSeller(Seller seller)
        {
            var sellerDoc = this.GetSellerDoc(seller);

            return await this._elasticSearchService.UpdateSeller(sellerDoc);
        }

        /// <summary>
        /// delete seller from search index
        /// </summary>
        /// <param name="id">seller id</param>
        public async Task<bool> DeleteSeller(Guid id)
        {
            var sellerDoc = new SellerDoc() { Id = id };

            return await this._elasticSearchService.DeleteSeller(sellerDoc);
        }

        /// <summary>
        /// search matches for inquiry
        /// </summary>
        /// <param name="inquiry">inquiry object</param>
        /// <param name="minScorePercentage">minimum score a match should have in relation to the highest matching score, matching results underneath this threshold are ignored</param>
        /// <param name="minMatchPercentageForCategories">minimum percentage of matching categories, 100 = all categories must match</param>
        /// <param name="minMatchPercentageForSubCategories">minimum percentage of matching subcategories, 100 = all subcategories must match</param>
        /// <returns></returns>
        public async Task<List<MatchItem>> SearchMatchesForInquiry(InquiryItem inquiry, int minScorePercentage = 0, double minMatchPercentageForCategories = 100, double minMatchPercentageForSubCategories = 0)
        {
            List<MatchItem> result = new();

            if (inquiry.IsCanceled) return result;

            try
            {
                var sellerHits = await this._elasticSearchService.GetMatchingSellersForInquiry(this.Mapper.Map<Inquiry>(inquiry), minScorePercentage, minMatchPercentageForCategories, minMatchPercentageForSubCategories);

                if (sellerHits.Any())
                {
                    var maxScore = sellerHits.Max(h => h.Score) != null ? sellerHits.Max(h => h.Score) : 0;

                    foreach (var sellerHit in sellerHits)
                    {
                        SellerDoc sellerMatch = sellerHit.Source;

                        MatchItem match = this.Mapper.Map<MatchItem>(this._matchservice.GetNewMatch(inquiry.Id, sellerHit.Source.Id));
                        match.Score = sellerHit.Score.HasValue && maxScore.HasValue ? Convert.ToInt32(Math.Floor(sellerHit.Score.Value / maxScore.Value * 100)) : 0;
                        match.Seller = this.Mapper.Map<SellerItem>(sellerMatch);

                        result.Add(match);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logService.CreateLog($"Fehler ElasticSearchManager/GetMatchesForInquiry. Project: {inquiry.ProjectId} " + ex.Message + " / " + ex.InnerException?.Message, this.Mapper.Map<Inquiry>(inquiry));

                return result;
            }

            return result;
        }

        /// <summary>
        /// get Seller with additional needed entities
        /// </summary>
        /// <param name="id"></param>
        private Seller GetSeller(Guid id)
        {
            Seller seller = this._companyService.GetSellerWithPortfolioAndGeodata(id);
                    
            return seller;
        }

        /// <summary>
        /// creates an ElasticSearch document from Seller entity
        /// </summary>
        private SellerDoc GetSellerDoc(Seller sellerCompany)
        {
            var sellerItem = new SellerDoc()
            {
                Id = sellerCompany.Id,
                Name = sellerCompany.Name,
                CategoryIds = sellerCompany.PortfolioCategories.Select(pc => pc.Id).ToList(),
                SubCategoryIds = sellerCompany.PortfolioSubCategories.Select(pc => pc.Id).ToList()
            };

            foreach (var category in sellerCompany.PortfolioCategories)
            {
                var categoryItem = new DL.Data.ElasticSearch.CategoryDoc
                {
                    Id = category.Id,
                    Name = category.Name,
                };

                var additionalTags = sellerCompany.AdditionalPortfolioCategoryTags.Where(ct => ct.PortfolioCategoryId == category.Id).FirstOrDefault();

                if (additionalTags != null)
                {
                    categoryItem.Tags = additionalTags.Tags;
                }

                sellerItem.Categories.Add(categoryItem);
            }

            foreach (var category in sellerCompany.PortfolioSubCategories)
            {
                var categoryItem = new DL.Data.ElasticSearch.CategoryDoc
                {
                    Id = category.Id,
                    Name = category.Name,
                };

                sellerItem.SubCategories.Add(categoryItem);
            }

            foreach (var community in sellerCompany.Communities)
            {
                foreach (var city in community.Cities)
                {
                    var cityItem = new DL.Data.ElasticSearch.CityDoc
                    {
                        CommunityName = community.Name,
                        PostalCode = city.PostalCode,
                        Name = city.Name,
                        Location = $"{city.Latitude.ToString(new CultureInfo("en-us", false))},{city.Longitude.ToString(new CultureInfo("en-us", false))}"
                    };

                    sellerItem.Cities.Add(cityItem);
                }
            }

            return sellerItem;
        }
    }
}