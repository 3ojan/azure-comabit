// <copyright file="PortfolioManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Porfolio
{
    using Comabit.BL.Porfolio.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL.Data.Portfolio;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class PortfolioManager : BaseManager
    {
        private IPortfolioService _portfolioService;

        public PortfolioManager(IPortfolioService portfolioService)
        {
            this._portfolioService = portfolioService;
        }

        public async Task<IEnumerable<PortfolioAreaItem>> RetrievePortfolioAreas(bool ShowAdditionalTags = false)
        {
            ICollection<PortfolioAreaItem> areas = this.Mapper.Map<ICollection<PortfolioAreaItem>>(await this._portfolioService.GetAllAreas().ToListAsync());

            areas.SelectMany(a => a.PortfolioCategories).ToList().ForEach(c => c.PortfolioArea.ShowAdditionalTags = ShowAdditionalTags);

            return areas;
        }

        public async Task<IEnumerable<PortfolioCategoryItem>> RetrievePortfolioCategories(Guid portfolioAreaId)
        {
            if (portfolioAreaId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(portfolioAreaId));
            }

            var portfolioCategoryEntities = await this._portfolioService.GetAllCategoriesByAreaId(portfolioAreaId).ToListAsync();

            var result = portfolioCategoryEntities.Select(e => this.Mapper.Map<PortfolioCategory, PortfolioCategoryItem>(e)).ToList();

            return result;
        }

        public async Task<IEnumerable<PortfolioSubCategoryItem>> RetrievePortfolioSubCategories(Guid portfolioAreaCategoryId)
        {
            if (portfolioAreaCategoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(portfolioAreaCategoryId));
            }

            var portfolioSubCategoryEntities = await this._portfolioService.GetSubCategoriesByCategoryId(portfolioAreaCategoryId).ToListAsync();

            var result = portfolioSubCategoryEntities.Select(e => this.Mapper.Map<DL.Data.Portfolio.PortfolioSubCategory, BL.Porfolio.Dto.PortfolioSubCategoryItem>(e)).ToList();

            return result;
        }
    }
}