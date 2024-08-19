// <copyright file="IMatchservice.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Match;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMatchService
    {
        IQueryable<Match> GetAll();

        void Add(Match match);

        void AddMany(List<Match> matches);

        Match GetNewMatch(Guid inquiryId, Guid sellerId);

        void Delete(Match match);

        IQueryable<Match> Get(Guid id);

        IQueryable<Match> GetMatchWithSellerCompany(Guid id);

        IQueryable<Match> GetSellerMatches(Guid sellerGuid);

        IQueryable<Offer> GetOffer(Guid Id);

        IQueryable<Offer> GetOffersForInquiry(Guid inquiryId);

        IQueryable<Offer> GetOffersForProject(Guid projectId);

        IQueryable<Offer> GetOffersForBuyer(Guid buyerId);

        IQueryable<Match> GetInquiryMatches(Guid inquiryId);

        void AddMessage(UserMessage message);

        Task SaveAsync();
    }
}