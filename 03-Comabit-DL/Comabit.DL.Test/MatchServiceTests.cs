// <copyright file="CompanyServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Company;
using Comabit.DL.Data.Inquiry;
using Comabit.DL.Data.Match;
using Comabit.DL.Data.Portfolio;
using Comabit.DL.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.DL.Test
{
    public class MatchserviceTests : BaseServiceTests
    {
        private MatchService _matchservice;

        private InquiryService _inquiryService;

        private CompanyService _companyService;

        [SetUp]
        public void Setup()
        {
            this._matchservice = new MatchService(this.UnitOfWork);
            this._inquiryService = new InquiryService(this.UnitOfWork);
            this._companyService = new CompanyService(this.UnitOfWork);
        }

        [Test, Order(1)]
        public async Task Add()
        {
            Buyer buyer = await this._companyService.GetAllBuyers().Where(b => b.Projects.Any(b => b.Inquiries.Any())).OrderByDescending(b => b.CreatedAt).FirstOrDefaultAsync();
            Project project = await this._inquiryService.GetBuyerProjectsByBuyerCompanyId(buyer.Id).FirstOrDefaultAsync();
            Inquiry inquiry = project.Inquiries.OrderByDescending(i => i.CreatedAt).FirstOrDefault();
            Seller seller = await this._companyService.GetAllSellers().Where(s => s.Users.Any(u => u.Email.Contains("seller@mission"))).FirstOrDefaultAsync();

            if (seller == null)
            {
                seller = await this._companyService.GetAllSellers().OrderByDescending(s => s.CreatedAt).FirstOrDefaultAsync();
            }

            Match match = this._matchservice.GetNewMatch(inquiry.Id, seller.Id);
            match.Id = new Guid("c79f8d81-7677-48ec-829e-15c76f81ca81");

            this._matchservice.Add(match);
            await this._matchservice.SaveAsync();

            Match matchEntity = await this._matchservice.Get(match.Id).FirstOrDefaultAsync();

            Assert.That(matchEntity != null);
        }

        [Test, Order(2)]
        public async Task Delete()
        {
            Guid matchId = new Guid("c79f8d81-7677-48ec-829e-15c76f81ca81");
            Match match = await this._matchservice.Get(matchId).FirstOrDefaultAsync();

            this._matchservice.Delete(match);
            await this._matchservice.SaveAsync();

            Match matchEntity = await _matchservice.Get(matchId).FirstOrDefaultAsync();

            Assert.That(matchEntity == null);
        }
    }
}