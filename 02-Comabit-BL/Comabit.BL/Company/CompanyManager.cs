using Comabit.BL.Company.Dto;
using Comabit.DL.Interfaces;
using Comabit.DL;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Comabit.BL.Shared;
using Comabit.DL.Data.Company;
using Microsoft.EntityFrameworkCore;
using Comabit.BL.Porfolio.Dto;
using Users.Infrastructure;
using Comabit.BL.ElasticSearch;
using Comabit.BL.Shared.DTO;

namespace Comabit.BL.Company
{
    public class CompanyManager : BaseManager
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ElasticSearchManager _elasticSearchManager;
        
        public CompanyManager(ICompanyService companyService, IMapper mapper, UserManager<ApplicationUser> userManager, ElasticSearchManager elasticSearchManager)
        {
            this._companyService = companyService;
            this._userManager = userManager;
            this._elasticSearchManager = elasticSearchManager;
        }


        public async Task<ICollection<SellerItem>> GetAllSeller()
        {
            ICollection<Seller> sellers = await this._companyService.GetAllSellers().ToListAsync();
            List<SellerItem> companies = this.Mapper.Map<List<SellerItem>>(sellers);
            companies.ForEach(c => c.Role = Roles.Seller);

            return companies;
        }

        public async Task<ICollection<CompanyItem>> GetAllBuyer()
        {
            ICollection<Buyer> buyers = await this._companyService.GetAllBuyers().ToListAsync();
            List<CompanyItem> companies = this.Mapper.Map<List<CompanyItem>>(buyers);
            companies.ForEach(c => c.Role = Roles.Buyer);

            return companies;
        }

        public CompanyItem Get(Guid guid)
        {
            return this.Mapper.Map<CompanyItem>(this._companyService.GetCompany(guid));
        }

        public CompanyItem GetForUser(string userId)
        {
            return this.Mapper.Map<CompanyItem>(this._companyService.GetCompanyByUserId(userId));
        }

        public async Task Update(CompanyItem company)
        {
            DL.Data.Company.Company companyEntity = this._companyService.GetCompany(company.Id);

            companyEntity.Name = company.Name;
            companyEntity.Street = company.Street;
            companyEntity.PostalCode = company.PostalCode;
            companyEntity.City = company.City;
            companyEntity.BusinessTaxId = company.BusinessTaxId;
            companyEntity.UstId = company.UstId;
            companyEntity.UpdatedAt = DateTime.Now;

            await this._companyService.SaveAsync();
            await this._elasticSearchManager.UpdateSeller(company.Id);
        }

        public async Task Delete(Guid guid)
        {
            this._companyService.DeleteCompany(guid);
            await this._companyService.SaveAsync();
            await this._elasticSearchManager.DeleteSeller(guid);
        }

        public async Task<CountOverAllResultItem> GetCountSellers(int days1, int days2)
        {
            ICollection<Seller> sellers = await this._companyService.GetAllSellers().ToListAsync();

            var fromDate = DateTime.Now.AddDays(days1 * -1);
            var countDays1Sellers = sellers.Where(o => o.CreatedAt >= fromDate).Count();
            fromDate = DateTime.Now.AddDays(days2 * -1);
            var countDays2Sellers = sellers.Where(o => o.CreatedAt >= fromDate).Count();

            var result = new CountOverAllResultItem()
            {
                CountOverAll = sellers.Count(),
                Days1 = days1,
                Days2 = days2,
                CountValue1 = countDays1Sellers,
                CountValue2 = countDays2Sellers
            };

            return result;
        }

        public async Task<ICollection<DateCountResultItem>> GetCountSellerByMonth(DateTime fromDate, DateTime toDate)
        {
            var result = new List<DateCountResultItem>();

            var sellerQuery = this._companyService.GetAllSellers();

            var currentFromDate = new DateTime(fromDate.Year, fromDate.Month, 1);
            var currentToDate = currentFromDate.AddMonths(1);
            while (currentFromDate < toDate)
            {
                var countSellers = await sellerQuery.Where(o => o.CreatedAt >= currentFromDate && o.CreatedAt < currentToDate).CountAsync();
                var resultItem = new DateCountResultItem()
                {
                    Date = currentFromDate,
                    CountValue = countSellers
                };
                result.Add(resultItem);
                currentFromDate = currentFromDate.AddMonths(1);
                currentToDate = currentFromDate.AddMonths(1);
            }

            return result;
        }

        public async Task<ICollection<DateCountResultItem>> GetCountBuyerByMonth(DateTime fromDate, DateTime toDate)
        {
            var result = new List<DateCountResultItem>();

            var buyerQuery = this._companyService.GetAllBuyers();

            var currentFromDate = new DateTime(fromDate.Year, fromDate.Month, 1);
            var currentToDate = currentFromDate.AddMonths(1);
            while (currentFromDate < toDate)
            {
                var countBuyer = await buyerQuery.Where(o => o.CreatedAt >= currentFromDate && o.CreatedAt < currentToDate).CountAsync();
                var resultItem = new DateCountResultItem()
                {
                    Date = currentFromDate,
                    CountValue = countBuyer
                };
                result.Add(resultItem);
                currentFromDate = currentFromDate.AddMonths(1);
                currentToDate = currentFromDate.AddMonths(1);
            }

            return result;
        }

        public async Task<CountOverAllResultItem> GetCountBuyers(int days1, int days2)
        {
            ICollection<Buyer> buyers = await this._companyService.GetAllBuyers().ToListAsync();

            var fromDate = DateTime.Now.AddDays(days1 * -1);
            var countDays1Buyers = buyers.Where(o => o.CreatedAt >= fromDate).Count();
            fromDate = DateTime.Now.AddDays(days2 * -1);
            var countDays2Sellers = buyers.Where(o => o.CreatedAt >= fromDate).Count();

            var result = new CountOverAllResultItem()
            {
                CountOverAll = buyers.Count(),
                Days1 = days1,
                Days2 = days2,
                CountValue1 = countDays1Buyers,
                CountValue2 = countDays2Sellers
            };

            return result;
        }
    }
}