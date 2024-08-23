// <copyright file="IPortfolioService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Portfolio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPortfolioService : IAsyncDisposable
    {
        IQueryable<PortfolioArea> GetAllAreas();

        IQueryable<PortfolioCategory> GetAllCategories();

        IQueryable<PortfolioCategory> GetCategoriesByIds(ICollection<Guid> ids);

        IQueryable<PortfolioSubCategory> GetSubCategoriesByIds(ICollection<Guid> ids);

        IQueryable<PortfolioCategory> GetAllCategoriesByAreaId(Guid portfolioAreaId);

        IQueryable<PortfolioSubCategory> GetSubCategoriesByCategoryId(Guid portfolioCategoryId);

        void AddAdditionalPortfolioCategoryTags(AdditionalPortfolioCategoryTags additionalPortfolioCategoryTags);

        Task<int> SaveAsync();
    }
}