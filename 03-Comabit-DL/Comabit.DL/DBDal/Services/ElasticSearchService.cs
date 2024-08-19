// <copyright file="ElasticSearchService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.ElasticSearch;
    using Comabit.DL.Interfaces;
    using Elasticsearch.Net;
    using Nest;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ElasticSearchService : IElasticSearchService
    {
        private readonly string Endpoint;

        private ElasticClient _elasticClient { get; set; }

        private ElasticClient ElasticClient
        {
            get
            {
                if (_elasticClient == null)
                {
                    var settings = new ConnectionSettings(new Uri(Endpoint))
                        .DefaultIndex("sellers")
                        .BasicAuthentication("elastic", "NB9zScJl9Z3zJytCdX1THxYi")
                        .EnableDebugMode();

                    _elasticClient = new ElasticClient(settings);
                }

                return _elasticClient;
            }
        }

        public ElasticSearchService()
        {
            Endpoint = "https://8e1667b965da466b9160eac8ed245a52.westeurope.azure.elastic-cloud.com:9243";
        }

        public async Task<long> GetAllSellersCount()
        {
            var response = await ElasticClient.SearchAsync<SellerDoc>(s => s
                .RequestConfiguration(r => r
                    .DisableDirectStreaming()
                )
            );

            return response.Total;
        }

        public async Task<bool> AddSeller(SellerDoc seller)
        {
            IndexResponse response = await ElasticClient.IndexDocumentAsync(seller);

            return response.IsValid;
        }

        public async Task<bool> UpdateSeller(SellerDoc seller)
        {
            var response = await ElasticClient.UpdateAsync<SellerDoc>(seller.Id, d => d
                .Index("sellers")
                .Doc(seller)
                .Refresh(Refresh.True)
            );

            return response.IsValid;
        }

        public async Task<bool> DeleteSeller(SellerDoc seller)
        {
            var response = await ElasticClient.DeleteAsync<SellerDoc>(seller.Id, d => d
                .Index("sellers")
                .Refresh(Refresh.True)
            );

            return response.IsValid;
        }

        /// <summary>
        /// get list of matching sellers for an inquiry
        /// </summary>
        /// <param name="inquiry"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IHit<SellerDoc>>> GetMatchingSellersForInquiry(Inquiry inquiry, double minScorePercentage = 0, double minMatchPercentageForCategories = 100, double minMatchPercentageForSubCategories = 80)
        {
            string postalCode = !string.IsNullOrEmpty(inquiry.DeliveryPostalCode) ? inquiry.DeliveryPostalCode : inquiry.Project.PostalCode;

            if (string.IsNullOrEmpty(postalCode)
                || (inquiry.PortfolioCategories.Count == 0 && inquiry.PortfolioSubCategories.Count == 0))
            {
                return new List<Hit<SellerDoc>>();
            }

            var minMatchCategories = Convert.ToInt16(Math.Floor(inquiry.PortfolioCategories.Count() * minMatchPercentageForCategories / 100));
            var minMatchSubCategories = Convert.ToInt16(Math.Floor(inquiry.PortfolioSubCategories.Count() * minMatchPercentageForSubCategories / 100));

            List<QueryContainer> mustQueries = new List<QueryContainer>();
            List<QueryContainer> shouldQueries = new List<QueryContainer>();

            if (!string.IsNullOrEmpty(postalCode))
            {
                mustQueries.Add(new NestedQuery()
                {
                    Name = "postalcode_query",
                    Path = Infer.Field<SellerDoc>(p => p.Cities),
                    Query = new TermQuery()
                    {
                        Field = Infer.Field<SellerDoc>(p => p.Cities.First().PostalCode),
                        Value = postalCode
                    }
                });
            }

            if (inquiry.PortfolioCategories.Count > 0)
            {
                mustQueries.Add(new TermsSetQuery()
                {
                    Name = "categories_query",
                    Field = Infer.Field<SellerDoc>(p => p.CategoryIds),
                    Terms = inquiry.PortfolioCategories.Select(c => c.Id.ToString()),
                    MinimumShouldMatchScript = new InlineScript($"Math.min(params.num_terms, {minMatchCategories})"),
                    Boost = 1.1
                });
            }

            if (inquiry.PortfolioSubCategories.Count > 0)
            {
                shouldQueries.Add(new TermsSetQuery()
                {
                    Name = "subcategories_query",
                    Field = Infer.Field<SellerDoc>(p => p.SubCategoryIds),
                    Terms = inquiry.PortfolioSubCategories.Select(c => c.Id.ToString()),
                    MinimumShouldMatchScript = new InlineScript($"Math.min(params.num_terms, {minMatchSubCategories})")
                });
            }

            BoolQuery boolQuery = new BoolQuery
            {
                Must = mustQueries,
                Should = shouldQueries
            };

            var sellerResponse = await ElasticClient.SearchAsync<SellerDoc>(s => s
                .RequestConfiguration(r => r.DisableDirectStreaming())
                .Query(q => boolQuery)
            );

            var minScore = minScorePercentage > 0 ? sellerResponse.MaxScore * minScorePercentage : 0;

            return sellerResponse.Hits.Where(h => h.Score >= minScore);
        }
    }
}