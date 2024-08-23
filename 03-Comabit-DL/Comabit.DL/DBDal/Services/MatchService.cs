// <copyright file="Matchservice.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Match;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MatchService : IMatchService
    {
        private IUnitOfWork unitOfWork;

        private readonly IGenericRepository<Match> _matchRepository;
        private readonly IGenericRepository<Offer> _offerRepository;
        private readonly IGenericRepository<UserMessage> _messageRepository;

        public MatchService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this._matchRepository = new GenericRepository<Match>(this.unitOfWork.DbContext);
            this._offerRepository = new GenericRepository<Offer>(this.unitOfWork.DbContext);
            this._messageRepository = new GenericRepository<UserMessage>(this.unitOfWork.DbContext);
        }

        public IQueryable<Match> GetAll()
        {
            return _matchRepository.GetAll();
        }

        public void Add(Match match)
        {
            this._matchRepository.Add(match);
        }

        public void AddMany(List<Match> matches)
        {
            foreach (var match in matches)
            { 
                this._matchRepository.Add(match);
            }
        }

        public void Delete(Match match)
        {
            this._matchRepository.Delete(match);
        }

        public Match GetNewMatch(Guid inquiryId, Guid sellerId)
        {
            var newMatch = new Match()
            {
                InquiryId = inquiryId,
                SellerId = sellerId,
                State = MatchState.pending,
                Score = 100,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return newMatch;
        }

        public async Task SaveAsync()
        {
            await this.unitOfWork.SaveAsync();
        }

        public IQueryable<Match> Get(Guid id)
        {
            return this._matchRepository.GetAll("Offers,Offers.File,Messages,Inquiry,Inquiry.Project.Buyer," +
                "Inquiry.Files," +
                "Inquiry.PortfolioCategories," +
                "Inquiry.PortfolioCategories.PortfolioArea," +
                "Inquiry.PortfolioSubCategories," +
                "Inquiry.PortfolioSubCategories.PortfolioCategory," +
                "Inquiry.PortfolioSubCategories.PortfolioCategory.PortfolioArea")
                .Where(m => m.Id == id);
        }

        public IQueryable<Match> GetMatchWithSellerCompany(Guid id)
        {
            return this._matchRepository.GetAll("Seller").Where(m => m.Id == id);
        }

        public IQueryable<Match> GetSellerMatches(Guid sellerGuid)
        {
            return this._matchRepository
                .GetAll("Offers,Messages,Inquiry," +
                "Offers.File," +
                "Inquiry.Files," +
                "Inquiry.Project," +
                "Inquiry.Project.Buyer," +
                "Inquiry.PortfolioCategories," +
                "Inquiry.PortfolioCategories.PortfolioArea," +
                "Inquiry.PortfolioSubCategories," +
                "Inquiry.PortfolioSubCategories.PortfolioCategory," +
                "Inquiry.PortfolioSubCategories.PortfolioCategory.PortfolioArea")
                .Where(m => m.SellerId == sellerGuid);
        }

        public IQueryable<Offer> GetOffer(Guid Id)
        {
            return this._offerRepository.GetAll(includeProperties: "Match,Match.Offers,Match.Offers.File,Match.Inquiry,Match.Inquiry.Files," +
                "Match.Inquiry.PortfolioCategories,Match.Inquiry.PortfolioCategories.PortfolioArea," +
                "Match.Inquiry.PortfolioSubCategories,Match.Inquiry.PortfolioSubCategories.PortfolioCategory,Match.Inquiry.PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "Match.Inquiry.Project,Match.Seller,File").Where(o => o.Id == Id);
        }

        public IQueryable<Offer> GetOffersForInquiry(Guid inquiryId)
        {
            return this._offerRepository.GetAll(includeProperties: "Match,Match.Inquiry," +
                "Match.Inquiry.PortfolioCategories,Match.Inquiry.PortfolioCategories.PortfolioArea," +
                "Match.Inquiry.PortfolioSubCategories,Match.Inquiry.PortfolioSubCategories.PortfolioCategory,Match.Inquiry.PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "Match.Inquiry.Project,Match.Seller,File")
                .Where(o => o.Match.InquiryId == inquiryId);
        }

        public IQueryable<Offer> GetOffersForProject(Guid projectId)
        {
            return this._offerRepository.GetAll(includeProperties: "Match,Match.Inquiry," +
                "Match.Inquiry.PortfolioCategories,Match.Inquiry.PortfolioCategories.PortfolioArea," +
                "Match.Inquiry.PortfolioSubCategories,Match.Inquiry.PortfolioSubCategories.PortfolioCategory,Match.Inquiry.PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "Match.Inquiry.Project,Match.Seller,File")
                .Where(o => o.Match.Inquiry.ProjectId == projectId);
        }

        public IQueryable<Match> GetInquiryMatches(Guid inquiryId)
        {
            return this._matchRepository.GetAll(includeProperties: "Seller")
                .Where(o => o.InquiryId == inquiryId);
        }

        public IQueryable<Offer> GetOffersForBuyer(Guid buyerId)
        {
            return this._offerRepository.GetAll(includeProperties: "Match,Match.Inquiry," +
                "Match.Inquiry.PortfolioCategories,Match.Inquiry.PortfolioCategories.PortfolioArea," +
                "Match.Inquiry.PortfolioSubCategories,Match.Inquiry.PortfolioSubCategories.PortfolioCategory,Match.Inquiry.PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "Match.Inquiry.Project,Match.Seller,File")
                .Where(o => o.Match.Inquiry.Project.BuyerId == buyerId);
        }

        public void AddMessage(UserMessage message)
        {
            this._messageRepository.Add(message);
        }
    }
}