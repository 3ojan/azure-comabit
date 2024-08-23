// <copyright file="CompanyService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Comabit.DL.Data.Portfolio;

    public class CompanyService : ICompanyService
    {
        private IUnitOfWork unitOfWork;

        private IGenericRepository<Company> _companyRepository;
        private IGenericRepository<Seller> _sellerCompanyRepository;
        private IGenericRepository<Buyer> _buyerCompanyRepository;
        private IGenericRepository<AdditionalPortfolioCategoryTags> _additionalTagsRepository;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this._companyRepository = new GenericRepository<Company>(this.unitOfWork.DbContext);
            this._sellerCompanyRepository = new GenericRepository<Seller>(this.unitOfWork.DbContext);
            this._buyerCompanyRepository = new GenericRepository<Buyer>(this.unitOfWork.DbContext);
            this._additionalTagsRepository = new GenericRepository<AdditionalPortfolioCategoryTags>(this.unitOfWork.DbContext);
        }

        public IQueryable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAll(includeProperties: "Users");
        }

        public IQueryable<Seller> GetAllSellers()
        {
            return _sellerCompanyRepository.GetAll(includeProperties: "Users");
        }

        public IQueryable<Buyer> GetAllBuyers()
        {
            return _buyerCompanyRepository.GetAll(includeProperties: "Users");
        }

        public Company GetCompany(Guid guid)
        {
            return _companyRepository.Get(c => c.Id == guid, includeProperties: "Users").SingleOrDefault();
        }

        public void DeleteCompany(Guid guid)
        {
            _companyRepository.Delete(this.GetCompany(guid));
        }

        public Company GetCompanyByUserId(string userId)
        {
            return _companyRepository.Get(c => c.Users.Any(u => u.Id == userId), includeProperties: "Users").SingleOrDefault();
        }

        public Seller GetSeller(string userId)
        {
            return _sellerCompanyRepository.Get(c => c.Users.Any(u => u.Id == userId), includeProperties: "Users").FirstOrDefault();
        }

        public Seller GetSellerWithPortfolioAndGeodata(Guid sellerId)
        {
            // Idee: 
            // *1 load seller without categories etc.
            // *2 load all n:m category-ids
            // *3 load all categories where id in (*2)
            // *4 load all n:m subcategory-ids
            // *5 load all subcategory where id in (*4)
            // *6 load all additionaltags where id in (*4)

            // Alternative, Workaround: jeweils nur ein bzw. "einfache" includes einzeln laden und als Eigenschaft von Seller hinzufügen

            Seller seller = _sellerCompanyRepository.Get(c => c.Id == sellerId, includeProperties: "PortfolioCategories").SingleOrDefault();
            seller.AdditionalPortfolioCategoryTags = this._additionalTagsRepository.GetAll().Where(t => t.SellerCompanyId == sellerId).ToList();
            seller.PortfolioSubCategories = _sellerCompanyRepository.Get(c => c.Id == sellerId, includeProperties: "PortfolioSubCategories").SingleOrDefault().PortfolioSubCategories;
            seller.Communities = _sellerCompanyRepository.Get(c => c.Id == sellerId, includeProperties: "Communities, Communities.Cities").SingleOrDefault().Communities;

            return seller;
        }

        public Buyer GetBuyerByUserId(string userId)
        {
            return _buyerCompanyRepository.Get(c => c.Users.Any(u => u.Id == userId)).FirstOrDefault();
        }

        public Buyer GetBuyerById(Guid buyerId)
        {
            var result = this._buyerCompanyRepository.Get(c => c.Id == buyerId).Single();

            return result;
        }

        public void AddBuyer(Buyer buyer)
        {
            this._buyerCompanyRepository.Add(buyer);
        }

        public void UpdateBuyer(Buyer buyer)
        {
            this._buyerCompanyRepository.Update(buyer);
        }

        public IQueryable<Seller> GetSellerPortfolio(Guid sellerId)
        {
            var result = this._sellerCompanyRepository.GetAll(includeProperties: "PortfolioCategories,PortfolioSubCategories,AdditionalPortfolioCategoryTags").Where(c => c.Id == sellerId);

            return result;
        }

        public IQueryable<Seller> GetSellerCompany(Guid sellerId)
        {
            var result = this._sellerCompanyRepository.GetAll(includeProperties: "PortfolioCategories,PortfolioSubCategories,AdditionalPortfolioCategoryTags,Communities").Where(c => c.Id == sellerId);

            return result;
        }

        public IQueryable<Seller> GetSellerCommunities(Guid sellerId)
        {
            var result = this._sellerCompanyRepository.GetAll(includeProperties: "Communities").Where(c => c.Id == sellerId);

            return result;
        }

        public void AddSeller(Seller seller)
        {
            this._sellerCompanyRepository.Add(seller);
        }

        public void UpdateSeller(Seller seller)
        {
            this._sellerCompanyRepository.Update(seller);
        }

        public async Task<int> SaveAsync()
        {
            return await unitOfWork.SaveAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}