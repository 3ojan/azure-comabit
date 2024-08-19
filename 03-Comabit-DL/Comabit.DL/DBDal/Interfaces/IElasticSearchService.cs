// <copyright file="IElasticSearchService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.ElasticSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Nest;

    public interface IElasticSearchService
    {
        Task<long> GetAllSellersCount();

        Task<bool> AddSeller(SellerDoc seller);

        Task<bool> UpdateSeller(SellerDoc seller);

        Task<bool> DeleteSeller(SellerDoc seller);

        Task<IEnumerable<IHit<SellerDoc>>> GetMatchingSellersForInquiry(Inquiry inquiry, double minScoreFactor = 0, double minMatchFactorForCategories = 1.0, double minMatchFactorForSubCategories = 0.8);
    }
}