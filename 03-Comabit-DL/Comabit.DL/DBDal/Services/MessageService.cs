// <copyright file="MessageService.cs" company="mission-one">
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
    using Comabit.DL.Data.Match;
    using System.Collections.Generic;

    public class MessageService : IMessageService
    {
        private IUnitOfWork unitOfWork;

        private IGenericRepository<Message> _messageRepository;
        private IGenericRepository<UserMessage> _userMessageRepository;
        private IGenericRepository<SystemMessage> _systemMessageRepository;
        private IGenericRepository<PendingReading> _pendingReadingRepository;
        


        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this._messageRepository = new GenericRepository<Message>(this.unitOfWork.DbContext);
            this._userMessageRepository = new GenericRepository<UserMessage>(this.unitOfWork.DbContext);
            this._systemMessageRepository = new GenericRepository<SystemMessage>(this.unitOfWork.DbContext);
            this._pendingReadingRepository = new GenericRepository<PendingReading>(this.unitOfWork.DbContext);
        }

        public IQueryable<Message> GetMessages(Guid matchId)
        {
            return this._messageRepository.GetAll("Match, Match.Offers, Match.Seller, Match.Inquiry, Match.Inquiry.Project, Match.Inquiry.Project.Buyer, PendingReadings").Where(m => m.MatchId.Equals(matchId));
        }

        public IQueryable<Message> GetMessagesForBuyer(Guid buyerId)
        {
            return this._messageRepository.GetAll("Match, Match.Offers, Match.Seller, Match.Inquiry, Match.Inquiry.Project, Match.Inquiry.Project.Buyer, PendingReadings").Where(m => m.Match.Inquiry.Project.BuyerId.Equals(buyerId));
        }

        public IQueryable<Message> GetMessagesForSeller(Guid sellerId)
        {
            return this._messageRepository.GetAll("Match, Match.Offers, Match.Seller, Match.Inquiry, Match.Inquiry.Project, Match.Inquiry.Project.Buyer, PendingReadings").Where(m => m.Match.SellerId.Equals(sellerId));
        }

        public UserMessage GetUserMessage(Guid messageId)
        {
            return this._userMessageRepository.GetAll("Match, Match.Offers, Match.Seller, Match.Inquiry, Match.Inquiry.Project, Match.Inquiry.Project.Buyer, PendingReadings").Where(m => m.Id.Equals(messageId)).FirstOrDefault();
        }


        public IQueryable<UserMessage> GetUserMessagesForMatch(Guid matchId)
        {
            return this._userMessageRepository.GetAll("Match, Match.Offers, Match.Seller, Match.Inquiry, Match.Inquiry.Project, Match.Inquiry.Project.Buyer, PendingReadings").Where(m => m.MatchId.Equals(matchId));
        }

        public IQueryable<SystemMessage> GetSystemMessagesForMatch(Guid matchId)
        {
            return this._systemMessageRepository.GetAll("Match, Match.Offers, Match.Seller, Match.Inquiry, Match.Inquiry.Project, Match.Inquiry.Project.Buyer, PendingReadings").Where(m => m.MatchId.Equals(matchId));
        }

        public void Add(UserMessage message)
        {
            this._userMessageRepository.Add(message);
        }

        public void Add(SystemMessage message)
        {
            this._systemMessageRepository.Add(message);
        }

        public async Task MarkAsRead(Guid messageId, Guid companyId)
        {
            var pendingReading = await this._pendingReadingRepository.GetAll().Where(o => o.MessageId.Equals(messageId) && o.CompanyId.Equals(companyId)).SingleOrDefaultAsync();
            if (pendingReading != null)
            {
                this._pendingReadingRepository.Delete(pendingReading);
            }
        }

        public async Task MarkAsRead(IEnumerable<Guid> messageIds, Guid companyId)
        {
            foreach (var id in messageIds)
            {
                await this.MarkAsRead(id, companyId);
            }
        }

        public int GetUnreadUserMessageCount(Guid userId)
        {
            return this._pendingReadingRepository.GetAll("Company").Where(o => o.IsUserMessage &&  o.Company.MainUserId.Equals(userId)).Count();
        }

        public int GetUnreadSystemMessageCount(Guid userId)
        {
            return this._pendingReadingRepository.GetAll("Company").Where(o => !o.IsUserMessage && o.Company.MainUserId.Equals(userId)).Count();
        }

        public IQueryable<Message> GetAll()
        {
            return this._messageRepository.GetAll("Match, Match.Seller, Match.Inquiry, Match.Inquiry.Project, PendingReadings");
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