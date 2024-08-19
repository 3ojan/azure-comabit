// <copyright file="PortfolioService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Portfolio;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class PortfolioService : IPortfolioService
    {
        private IUnitOfWork unitOfWork;

        private readonly IGenericRepository<PortfolioArea> _portfolioAreaRepository;
        private readonly IGenericRepository<PortfolioCategory> _portfolioCategoryRepository;
        private readonly IGenericRepository<PortfolioSubCategory> _portfolioSubCategoryRepository;

        private readonly IGenericRepository<AdditionalPortfolioCategoryTags> _additionalPortfolioCategoryTagsRepository;

        public PortfolioService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this._portfolioCategoryRepository = new GenericRepository<PortfolioCategory>(this.unitOfWork.DbContext);
            this._portfolioAreaRepository = new GenericRepository<PortfolioArea>(this.unitOfWork.DbContext);
            this._portfolioSubCategoryRepository = new GenericRepository<PortfolioSubCategory>(this.unitOfWork.DbContext);
            this._additionalPortfolioCategoryTagsRepository = new GenericRepository<AdditionalPortfolioCategoryTags>(this.unitOfWork.DbContext);
        }

        public IQueryable<PortfolioArea> GetAllAreas()
        {
            return this._portfolioAreaRepository.GetAll(includeProperties: "PortfolioCategories,PortfolioCategories.PortfolioArea,PortfolioCategories.PortfolioSubCategories");
        }

        public IQueryable<PortfolioCategory> GetAllCategories()
        {
            return this._portfolioCategoryRepository.GetAll(includeProperties: "PortfolioArea,PortfolioAreaSubCategories");
        }

        public IQueryable<PortfolioCategory> GetAllCategoriesByAreaId(Guid portfolioAreaId)
        {
            return this._portfolioCategoryRepository.GetAll(includeProperties: "AdditionalPortfolioCategoryTags").Where(c => c.PortfolioAreaId == portfolioAreaId);
        }

        public IQueryable<PortfolioSubCategory> GetSubCategoriesByCategoryId(Guid portfolioCategoryId)
        {
            return this._portfolioSubCategoryRepository.GetAll().Where(c => c.PortfolioCategoryId == portfolioCategoryId);
        }

        public void AddAdditionalPortfolioCategoryTags(AdditionalPortfolioCategoryTags additionalPortfolioCategoryTags)
        {
            this._additionalPortfolioCategoryTagsRepository.Add(additionalPortfolioCategoryTags);
        }

        public async Task<int> SaveAsync()
        {
            return await unitOfWork.SaveAsync();
        }

        public IQueryable<PortfolioCategory> GetCategoriesByIds(ICollection<Guid> ids)
        {
            return this._portfolioCategoryRepository.FindBy(c => ids.Contains(c.Id));
        }

        public IQueryable<PortfolioSubCategory> GetSubCategoriesByIds(ICollection<Guid> ids)
        {
            return this._portfolioSubCategoryRepository.FindBy(c => ids.Contains(c.Id));
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}