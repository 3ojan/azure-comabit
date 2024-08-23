// <copyright file="MatchManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Match
{
    using Comabit.BL.Inquiry.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Comabit.DL.Data.Match;
    using System.Linq;
    using Comabit.BL.Match.Dto;
    using Comabit.DL.Data.Inquiry;
    using System.IO;
    using Comabit.DL.Data.File;
    using Comabit.BL.Shared.DTO;
    using Comabit.BL.ElasticSearch;

    public class MatchManager : BaseManager
    {
        private IMatchService _matchService;
        private ICompanyService _companyService;
        private IElasticSearchService _elasticSearchService;
        private ILogService _logService;
        private ElasticSearchManager _elasticSearchManager;

        public MatchManager(IMatchService matchservice, ICompanyService companyService, IElasticSearchService elasticSearchService, ILogService logService)
        {
            this._matchService = matchservice;
            this._companyService = companyService;
            this._elasticSearchService = elasticSearchService;
            this._logService = logService;
            this._elasticSearchManager = new ElasticSearchManager(this._logService, this._companyService, this._elasticSearchService, this._matchService);
        }

        public async Task<ICollection<MatchItem>> GetSellerMatches(Guid sellerGuid, ICollection<MatchState> states)
        {
            return await GetSellerMatches(sellerGuid, states, -1);
        }

        public async Task<ICollection<MatchItem>> GetSellerMatches(Guid sellerGuid, ICollection<MatchState> states, int limit)
        {
            var matchesQuery = this._matchService.GetSellerMatches(sellerGuid);

            if (states != null)
            {
                matchesQuery = matchesQuery.Where(m => states.Contains(m.State));
            }

            if (limit > 0)
            {
                matchesQuery = matchesQuery.Take(limit);
            }

            var matches = await matchesQuery.OrderByDescending(m => m.CreatedAt).ToListAsync();

            ICollection<MatchItem> matchItems = this.Mapper.Map<ICollection<MatchItem>>(matchesQuery);

            return matchItems;
        }

        public async Task<ICollection<DateCountResultItem>> GetCountSellerMatchesByMonth(Guid sellerGuid, DateTime fromDate, DateTime toDate, ICollection<MatchState> states)
        {
            var result = new List<DateCountResultItem>();

            var matchesQuery = this._matchService.GetSellerMatches(sellerGuid);
            if (states != null)
            {
                matchesQuery = matchesQuery.Where(m => states.Contains(m.State));
            }

            var currentFromDate = new DateTime(fromDate.Year, fromDate.Month, 1);
            var currentToDate = currentFromDate.AddMonths(1);
            while (currentFromDate < toDate)
            {
                var countMatches = await matchesQuery.Where(o => o.CreatedAt >= currentFromDate && o.CreatedAt < currentToDate).CountAsync();
                var resultItem = new DateCountResultItem()
                {
                    Date = currentFromDate,
                    CountValue = countMatches
                };
                result.Add(resultItem);
                currentFromDate = currentFromDate.AddMonths(1);
                currentToDate = currentFromDate.AddMonths(1);
            }

            return result;
        }

        public async Task<DaysCountResultItem> GetCountSellerMatchesByDays(Guid sellerGuid, ICollection<MatchState> states, int days1, int days2)
        {
            var matchesQuery = this._matchService.GetSellerMatches(sellerGuid);

            if (states != null)
            {
                matchesQuery = matchesQuery.Where(m => states.Contains(m.State));
            }

            var fromDate = DateTime.Now.AddDays(days1 * -1);
            var countDays1Matches = await matchesQuery.Where(o => o.CreatedAt >= fromDate).CountAsync();
            fromDate = DateTime.Now.AddDays(days2 * -1);
            var countDays2Matches = await matchesQuery.Where(o => o.CreatedAt >= fromDate).CountAsync();

            var result = new DaysCountResultItem()
            {
                Days1 = days1,
                Days2 = days2,
                CountValue1 = countDays1Matches,
                CountValue2 = countDays2Matches
            };

            return result;
        }

        public async Task<ICollection<DateCountResultItem>> GetCountBuyerOffersByMonth(Guid buyerGuid, DateTime fromDate, DateTime toDate, ICollection<OfferState> states)
        {
            var result = new List<DateCountResultItem>();

            var offersQuery = this._matchService.GetOffersForBuyer(buyerGuid);
            if (states != null)
            {
                offersQuery = offersQuery.Where(m => states.Contains(m.State));
            }

            var currentFromDate = new DateTime(fromDate.Year, fromDate.Month, 1);
            var currentToDate = currentFromDate.AddMonths(1);
            while (currentFromDate < toDate)
            {
                var countMatches = await offersQuery.Where(o => o.CreatedAt >= currentFromDate && o.CreatedAt < currentToDate).CountAsync();
                var resultItem = new DateCountResultItem()
                {
                    Date = currentFromDate,
                    CountValue = countMatches
                };
                result.Add(resultItem);
                currentFromDate = currentFromDate.AddMonths(1);
                currentToDate = currentFromDate.AddMonths(1);
            }

            return result;
        }

        public async Task<DaysCountResultItem> GetCountBuyerOffersByDays(Guid buyerGuid, ICollection<OfferState> states, int days1, int days2)
        {
            var offersQuery = this._matchService.GetOffersForBuyer(buyerGuid);

            if (states != null)
            {
                offersQuery = offersQuery.Where(m => states.Contains(m.State));
            }

            var fromDate = DateTime.Now.AddDays(days1 * -1);
            var countDays1Offers = await offersQuery.Where(o => o.CreatedAt >= fromDate).CountAsync();
            fromDate = DateTime.Now.AddDays(days2 * -1);
            var countDays2Offers = await offersQuery.Where(o => o.CreatedAt >= fromDate).CountAsync();

            var result = new DaysCountResultItem()
            {
                Days1 = days1,
                Days2 = days2,
                CountValue1 = countDays1Offers,
                CountValue2 = countDays2Offers
            };

            return result;
        }

        public async Task UpdateOffer(Guid offerId, OfferState state)
        {
            Offer offer = await _matchService.GetOffer(offerId).FirstOrDefaultAsync();
            offer.State = state;

            if (state == OfferState.ordered)
            {
                offer.Match.State = MatchState.ordered;

                if (offer.Match.Inquiry.PlacingState == PlacingState.Open)
                {
                    offer.Match.Inquiry.PlacingState = PlacingState.PartialPlaced;
                }
            }
            else if (state == OfferState.renew)
            {
                offer.Match.State = MatchState.renew;
            }
            else if (state == OfferState.revoked)
            {
                offer.Match.State = MatchState.revoked;
            }

            await _matchService.SaveAsync();
        }

        public async Task SaveOfferNote(Guid offerId, string note)
        {
            Offer offer = await _matchService.GetOffer(offerId).FirstOrDefaultAsync();
            offer.BuyerNote = note;

            await _matchService.SaveAsync();
        }

        public async Task<OfferItem> GetOffer(Guid offerId)
        {
            return this.Mapper.Map<OfferItem>(await _matchService.GetOffer(offerId).FirstOrDefaultAsync());
        }

        public async Task<ICollection<OfferItem>> GetOffersForBuyer(Guid buyerId)
        {
            ICollection<Offer> offers = await _matchService.GetOffersForBuyer(buyerId)
                .OrderBy(o => o.Match.Inquiry.Deadline).ToListAsync();

            return this.Mapper.Map<ICollection<OfferItem>>(offers);
        }

        public async Task<ICollection<OfferItem>> GetOffersForBuyerWithLimit(Guid buyerId, int limit)
        {
            ICollection<Offer> offers = await _matchService.GetOffersForBuyer(buyerId)
                .OrderBy(o => o.Match.Inquiry.Deadline).Take(limit).ToListAsync();

            return this.Mapper.Map<ICollection<OfferItem>>(offers);
        }

        public async Task<ICollection<OfferItem>> GetOrdersForBuyer(Guid buyerId)
        {
            ICollection<Offer> offers = await _matchService.GetOffersForBuyer(buyerId)
                .Where(o => o.State == OfferState.ordered)
                .OrderByDescending(o => o.CreatedAt).ToListAsync();

            return this.Mapper.Map<ICollection<OfferItem>>(offers);
        }

        public async Task<ICollection<OfferItem>> GetOffersForInquiry(Guid inquiryId)
        {
            ICollection<Offer> offers = await _matchService.GetOffersForInquiry(inquiryId)
                .OrderBy(o => o.Match.Inquiry.Deadline).ToListAsync();

            return this.Mapper.Map<ICollection<OfferItem>>(offers);
        }

        public async Task<ICollection<OfferItem>> GetOffersForProject(Guid projectId)
        {
            ICollection<Offer> offers = await _matchService.GetOffersForProject(projectId)
                .OrderBy(o => o.Match.Inquiry.Deadline).ToListAsync();

            return this.Mapper.Map<ICollection<OfferItem>>(offers);
        }

        public async Task<MatchItem> GetSellerMatch(Guid id)
        {
            Match match = await this._matchService.Get(id).FirstOrDefaultAsync();
            MatchItem matchItem = this.Mapper.Map<MatchItem>(match);

            return matchItem;
        }

        public async Task<ICollection<MatchItem>> GetInquiryMatches(Guid inquiryId)
        {
            return this.Mapper.Map<ICollection<MatchItem>>(await this._matchService.GetInquiryMatches(inquiryId).ToListAsync());
        }

        public async Task AddOffer(CreateOfferItem createOfferItem)
        {
            Match match = await this._matchService.Get(createOfferItem.MatchId).FirstOrDefaultAsync();
            match.State = MatchState.offered;

            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                await createOfferItem.File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            match.Offers.Add(new Offer()
            {
                MatchId = createOfferItem.MatchId,
                Text = createOfferItem.Message,
                CreatedAt = DateTime.Now,
                File = new OfferFile()
                {
                    FileName = createOfferItem.File.FileName,
                    MimeType = createOfferItem.File.ContentType,
                    Size = Convert.ToInt32(createOfferItem.File.Length),
                    FileData = new FileData()
                    {
                        Data = fileBytes,
                    }
                },
            });

            await this._matchService.SaveAsync();
        }

        public async Task Restore(Guid id)
        {
            Match match = await this._matchService.Get(id).FirstOrDefaultAsync();
            match.State = MatchState.pending;
            match.RevokeReason = null;
            match.RevokeReasonText = string.Empty;

            await this._matchService.SaveAsync();
        }

        public async Task SetOfferState(Guid id, OfferState offerState)
        {
            var offer = await this._matchService.GetOffer(id).SingleOrDefaultAsync();

            if (offerState == OfferState.ordered)
            {
                offer.Match.State = MatchState.ordered;
            }

            offer.State = offerState;

            await this._matchService.SaveAsync();
        }

        public async Task AddMessage(MessageItem messageItem)
        {
            var matchEntity = await this._matchService.GetMatchWithSellerCompany(messageItem.MatchId).SingleOrDefaultAsync();

            var messageEntity = this.Mapper.Map<MessageItem, UserMessage>(messageItem);

            messageEntity.Id = Guid.NewGuid();
            messageEntity.Match = matchEntity;
            messageEntity.MatchId = matchEntity.Id;
            messageEntity.CreatedAt = DateTime.Now;
            messageEntity.ToUser = new Guid(matchEntity.Seller.MainUserId);

            this._matchService.AddMessage(messageEntity);

            await this._matchService.SaveAsync();
        }

        public async Task Revoke(Guid id, int revokeReason, string revokeReasonText)
        {
            Match match = await this._matchService.Get(id).FirstOrDefaultAsync();

            match.RevokeReason = Enum.Parse<RevokeReason>(revokeReason.ToString());
            match.RevokeReasonText = revokeReasonText;
            match.State = MatchState.revoked;

            await this._matchService.SaveAsync();
        }

        public async Task AddToSales(Guid id)
        {
            Match match = await this._matchService.Get(id).FirstOrDefaultAsync();
            match.State = MatchState.accepted;

            await this._matchService.SaveAsync();
        }

        /// <summary>
        /// find and save matches for inquiry
        /// </summary>
        /// <param name="inquiry">inquiry object</param>
        /// <param name="minScorePercentage">minimum score a match should have in relation to the highest matching score, matching results underneath this threshold are ignored</param>
        /// <param name="minMatchPercentageForCategories">minimum percentage of matching categories, 100 = all categories must match</param>
        /// <param name="minMatchPercentageForSubCategories">minimum percentage of matching subcategories, 100 = all subcategories must match</param>
        /// <returns></returns>
        public async Task<bool> CreateMatchesForInquiry(InquiryItem inquiry, int minScorePercentage = 0, double minMatchPercentageForCategories = 1, double minMatchPercentageForSubCategories = 1)
        {
            if (inquiry.IsCanceled) return false;

            List<MatchItem> matches = await this._elasticSearchManager.SearchMatchesForInquiry(inquiry, minScorePercentage, minMatchPercentageForCategories, minMatchPercentageForSubCategories);

            bool result = false;

            try
            {
                if (matches.Any() && inquiry.Id != Guid.Empty)
                {
                    var existingMatches = this._matchService.GetAll().Where(m => m.InquiryId == inquiry.Id).ToList();
                    var existingMatchSellerIds = existingMatches.Select(m => m.SellerId).ToList();
                    var excludedSellerIds = inquiry.ExcludedSellers.Select(s => s.SellerId).ToList();

                    foreach (var match in matches)
                    {
                        if (!existingMatchSellerIds.Contains(match.SellerId) && !excludedSellerIds.Contains(match.SellerId))
                        {
                            var newMatch = this._matchService.GetNewMatch(inquiry.Id, match.SellerId);

                            this._matchService.Add(newMatch);
                        }
                    }

                    foreach (var match in existingMatches)
                    {
                        if (excludedSellerIds.Contains(match.SellerId))
                        {
                            this._matchService.Delete(match);
                        }
                    }

                    await this._matchService.SaveAsync();
                }

                result = result || matches.Any();
            }
            catch (Exception ex)
            {
                result = false;
            }

            if (!result)
            {
                this._logService.CreateLog("Kein Match gefunden", this.Mapper.Map<Inquiry>(inquiry));
                await this._logService.SaveAsync();
            }

            return result;
        }
    }
}