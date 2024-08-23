using Comabit.DL.Data.Portfolio;
using Comabit.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.DBDal.Services
{
    public class PortfolioServiceMock : IPortfolioService
    {
        public IQueryable<PortfolioArea> GetAllAreas()
        {
            var result = new List<PortfolioArea>();

            for (int i = 1; i < 5; i++)
            {
                var item = new PortfolioArea()
                {
                    Id = Guid.NewGuid(),
                    PortfolioCategories = null,
                    Name = "Area_" + i
                };

                result.Add(item);
            }

            return result.AsQueryable();
        }

        public IQueryable<PortfolioCategory> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public IQueryable<PortfolioCategory> GetAllCategoriesByAreaId(Guid portfolioAreaId)
        {
            var result = new List<PortfolioCategory>();

            for (int i = 1; i < 10; i++)
            {
                var item = new PortfolioCategory()
                {
                    Id = Guid.NewGuid(),
                    PortfolioArea = null,
                    PortfolioAreaId = Guid.NewGuid(),
                    Name = "Category_" + i
                };

                result.Add(item);
            }

            return result.AsQueryable();
        }

        public IQueryable<PortfolioSubCategory> GetSubCategoriesByCategoryId(Guid portfolioAreaCategoryId)
        {
            var result = new List<PortfolioSubCategory>();

            for (int i = 1; i < 8; i++)
            {
                var item = new PortfolioSubCategory()
                {
                    Id = Guid.NewGuid(),
                    PortfolioCategoryId = Guid.NewGuid(),
                    PortfolioCategory = null,
                    Name = "SubCategory_" + i
                };

                result.Add(item);
            }

            return result.AsQueryable();
        }


        public IQueryable<PortfolioCategory> GetCategoriesByIds(ICollection<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PortfolioSubCategory> GetSubCategoriesByIds(ICollection<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async ValueTask DisposeAsync()
        {
            // Async cleanup mock
            await Task.Yield();
        }

        public void AddAdditionalPortfolioCategoryTags(AdditionalPortfolioCategoryTags additionalPortfolioCategoryTags)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}