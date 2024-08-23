// <copyright file="IPermissionService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Company;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICompanyService : IAsyncDisposable
    {
        IQueryable<Company> GetAllCompanies();

        IQueryable<Buyer> GetAllBuyers();

        IQueryable<Seller> GetAllSellers();

        IQueryable<Seller> GetSellerPortfolio(Guid sellerId);

        IQueryable<Seller> GetSellerCommunities(Guid sellerId);

        Company GetCompany(Guid guid);

        Company GetCompanyByUserId(string userId);

        Seller GetSeller(string userId);

        Seller GetSellerWithPortfolioAndGeodata(Guid id);

        Buyer GetBuyerByUserId(string userId);

        Buyer GetBuyerById(Guid buyerId);

        void AddBuyer(Buyer buyer);

        void UpdateBuyer(Buyer buyer);

        IQueryable<Seller> GetSellerCompany(Guid sellerId);

        void AddSeller(Seller seller);

        void UpdateSeller(Seller seller);

        void DeleteCompany(Guid guid);

        Task<int> SaveAsync();
        
    }
}