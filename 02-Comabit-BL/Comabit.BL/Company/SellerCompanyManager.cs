using Comabit.BL.Company.Dto;
using Comabit.BL.Shared;
using Comabit.DL.Data.Company;
using Comabit.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Comabit.BL.Porfolio.Dto;
using Comabit.DL.Data.Portfolio;
using Comabit.DL;
using Comabit.BL.Identity.Dto;
using Comabit.DL.Data.Geo;
using Comabit.BL.Geo.Dto;
using Comabit.BL.ElasticSearch;

namespace Comabit.BL.Company
{
    public class SellerCompanyManager : BaseManager
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ICompanyService _companyService;
        private readonly IGeoService _geoService;
        private readonly ElasticSearchManager _elasticSearchManager;
        private readonly IMatchService _matchservice;
        
        public SellerCompanyManager(ILogService logService, IGeoService geoService, IPortfolioService portfolioService, ICompanyService companyService, IMatchService matchservice, IElasticSearchService elasticSearchService)
        {
            this._portfolioService = portfolioService;
            this._companyService = companyService;
            this._geoService = geoService;
            this._matchservice = matchservice;
            this._elasticSearchManager = new ElasticSearchManager(logService ,companyService, elasticSearchService, matchservice);
        }

        public async Task<SellerItem> GetSellerCompany(Guid id)
        {
            var entity = await this._companyService.GetSellerCompany(id).FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "SellerCompany not found with id: '{0}'", id));
            }

            var result = this.Mapper.Map<Seller, SellerItem>(entity);

            return result;
        }

        public async Task CreateSellerCompany(RegisterSellerItem item, ApplicationUser user)
        {
            Seller sellerCompany = new Seller()
            {
                Id = Guid.NewGuid(),
                BusinessTaxId = item.BusinessTaxId,
                City = item.City,
                Confirmed = false,
                CreatedAt = DateTime.Now,
                MainUserId = user.Id,
                Name = item.Company,
                PostalCode = item.PostalCode,
                Street = item.Street,
                UpdatedAt = DateTime.Now,
                UstId = item.UstId,
                Users = new List<ApplicationUser>() { user },
            };

            sellerCompany.AdditionalPortfolioCategoryTags = this.GetPortfolioAdditionalTags(item.PortfolioAreas);
            sellerCompany.PortfolioCategories = await this.GetPortfolioCategories(item.PortfolioAreas);
            sellerCompany.PortfolioSubCategories = await this.GetPortfolioSubCategories(item.PortfolioAreas);

            sellerCompany.Communities = await this.GetCompanyCommunities(item.Communities);

            this._companyService.AddSeller(sellerCompany);
            await this._companyService.SaveAsync();

            await this._elasticSearchManager.AddSeller(sellerCompany.Id);
        }

        public async Task CreateBuyerCompany(RegisterBaseItem item, ApplicationUser user)
        {
            Buyer buyerCompany = new Buyer()
            {
                Id = Guid.NewGuid(),
                BusinessTaxId = item.BusinessTaxId,
                City = item.City,
                Confirmed = false,
                CreatedAt = DateTime.Now,
                MainUserId = user.Id,
                Name = item.Company,
                PostalCode = item.PostalCode,
                Street = item.Street,
                UpdatedAt = DateTime.Now,
                UstId = item.UstId,
                Users = new List<ApplicationUser>() { user },
            };

            this._companyService.AddBuyer(buyerCompany);
            await this._companyService.SaveAsync();
        }

        private async Task<ICollection<PortfolioCategory>> GetPortfolioCategories(ICollection<PortfolioAreaItem> portfolioAreas)
        {
            ICollection<PortfolioCategory> portfolioCategories = new List<PortfolioCategory>();

            if (portfolioAreas != null && portfolioAreas.Any(a => a.PortfolioCategories.Any(c => c.Checked)))
            {
                List<Guid> categoryIds = new List<Guid>();

                foreach (PortfolioCategoryItem categoryItem in portfolioAreas.SelectMany(a => a.PortfolioCategories.Where(c => c.Checked).ToList()).ToList())
                {
                    categoryIds.Add(categoryItem.Id);
                }

                portfolioCategories = await this._portfolioService.GetCategoriesByIds(categoryIds).ToListAsync();
            }

            return portfolioCategories;
        }

        private ICollection<AdditionalPortfolioCategoryTags> GetPortfolioAdditionalTags(ICollection<PortfolioAreaItem> portfolioAreas)
        {
            ICollection<AdditionalPortfolioCategoryTags> tags = new List<AdditionalPortfolioCategoryTags>();

            if (portfolioAreas != null && portfolioAreas.Any(a => a.PortfolioCategories.Any(c => c.Checked)))
            {
                foreach (PortfolioCategoryItem categoryItem in portfolioAreas.SelectMany(a => a.PortfolioCategories.Where(c => c.Checked).ToList()).ToList())
                {
                    if (!string.IsNullOrEmpty(categoryItem.AdditionalPortfolioCategoryTagsAsString))
                    {
                        AdditionalPortfolioCategoryTags tag = new AdditionalPortfolioCategoryTags()
                        {
                            Id = Guid.NewGuid(),
                            Tags = categoryItem.AdditionalPortfolioCategoryTagsAsString,
                            PortfolioCategoryId = categoryItem.Id,
                        };

                        tags.Add(tag);

                        this._portfolioService.AddAdditionalPortfolioCategoryTags(tag);
                    }
                }
            }

            return tags;
        }

        private async Task<ICollection<PortfolioSubCategory>> GetPortfolioSubCategories(ICollection<PortfolioAreaItem> portfolioAreas)
        {
            ICollection<PortfolioSubCategory> portfolioSubCategories = new List<PortfolioSubCategory>();

            if (portfolioAreas != null && portfolioAreas.Any(a => a.PortfolioCategories.Any(c => c.Checked)))
            {
                if (portfolioAreas.Any(a => a.PortfolioCategories.Any(c => c.PortfolioSubCategories.Any(s => s.Checked))))
                {
                    List<Guid> subCategoryIds = new List<Guid>();

                    foreach (Guid subCategoryId in portfolioAreas.SelectMany(a => a.PortfolioCategories.SelectMany(c => c.PortfolioSubCategories)).Where(c => c.Checked).Select(c => c.Id).ToList())
                    {
                        subCategoryIds.Add(subCategoryId);
                    }

                    portfolioSubCategories = await this._portfolioService.GetSubCategoriesByIds(subCategoryIds).ToListAsync();
                }
            }

            return portfolioSubCategories;
        }

        private async Task<ICollection<Community>> GetCompanyCommunities(ICollection<CommunityItem> communities)
        {
            if (communities != null && communities.Any())
            {
                var result = await _geoService.GetCommunitiesByIds(communities.Select(c => c.Id).ToList()).ToListAsync();

                return result;
            }

            return null;
        }

        public async Task<ICollection<PortfolioAreaItem>> GetPortfolioForEdit(Guid companyId)
        {
            SellerItem sellerCompany = this.Mapper.Map<SellerItem>(await this._companyService.GetSellerPortfolio(companyId).FirstOrDefaultAsync());

            ICollection<PortfolioAreaItem> portfolios = this.Mapper.Map<ICollection<PortfolioAreaItem>>(await this._portfolioService.GetAllAreas().ToListAsync());
            portfolios.SelectMany(a => a.PortfolioCategories).ToList().ForEach(p => p.PortfolioArea.ShowAdditionalTags = true);

            foreach (PortfolioCategoryItem cateory in portfolios.SelectMany(p => p.PortfolioCategories))
            {
                if (sellerCompany.PortfolioCategories.Any(c => c.Id == cateory.Id))
                {
                    cateory.Checked = true;

                    if (sellerCompany.AdditionalPortfolioCategoryTags != null && sellerCompany.AdditionalPortfolioCategoryTags.Any(c => c.PortfolioCategoryId == cateory.Id))
                    {
                        cateory.AdditionalPortfolioCategoryTagsAsString = sellerCompany.AdditionalPortfolioCategoryTags.Where(c => c.PortfolioCategoryId == cateory.Id).FirstOrDefault().Tags;
                    }
                }
            }

            foreach (PortfolioSubCategoryItem subCateory in portfolios.SelectMany(p => p.PortfolioCategories.SelectMany(c => c.PortfolioSubCategories)))
            {
                if (sellerCompany.PortfolioSubCategories.Any(c => c.Id == subCateory.Id))
                {
                    subCateory.Checked = true;
                }
            }

            return portfolios;
        }

        public async Task<ICollection<CommunityItem>> GetCommunitiesForEdit(Guid companyId)
        {
            SellerItem sellerCompany = this.Mapper.Map<SellerItem>(await this._companyService.GetSellerCommunities(companyId).FirstOrDefaultAsync());

            return sellerCompany.Communities;
        }

        public async Task UpdatePortfolio(Guid companyId, ICollection<PortfolioAreaItem> portfolios)
        {
            Seller company = await this._companyService.GetSellerPortfolio(companyId).FirstOrDefaultAsync();

            company.AdditionalPortfolioCategoryTags = this.GetPortfolioAdditionalTags(portfolios);
            company.PortfolioCategories = await this.GetPortfolioCategories(portfolios);
            company.PortfolioSubCategories = await this.GetPortfolioSubCategories(portfolios);

            this._companyService.UpdateSeller(company);
            await this._companyService.SaveAsync();

            await this._elasticSearchManager.UpdateSeller(company.Id);
        }

        public async Task UpdateCommunities(Guid companyId, ICollection<CommunityItem> communities)
        {
            Seller company = await this._companyService.GetSellerCompany(companyId).FirstOrDefaultAsync();

            company.Communities = await this.GetCompanyCommunities(communities);

            this._companyService.UpdateSeller(company);
            await this._companyService.SaveAsync();

            await this._elasticSearchManager.UpdateSeller(company.Id);

        }
    }
}