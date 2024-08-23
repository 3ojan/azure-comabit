// <copyright file="MessageManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Message
{
    using Comabit.BL.Message.DTO;
    using Comabit.BL.Shared;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Comabit.DL.Data.Match;
    using Comabit.DL.Data.Company;

    public class MessageManager : BaseManager
    {
        private IMessageService _messageService;
        private ICompanyService _companyService;
        private IMatchService _matchService;

        public MessageManager(IMessageService messageService, ICompanyService companyService, IMatchService matchService)
        {
            this._messageService = messageService;
            this._companyService = companyService;
            this._matchService = matchService;
        }

        public async Task<ICollection<MatchChatItem>> GetMatchChatsForBuyer(Guid buyerId, Guid? matchId = null)
        {
            var messages = await this._messageService.GetMessagesForBuyer(buyerId).ToListAsync();
            var result = GetMatchChatItems(messages, buyerId);

            return result;
        }

        public async Task<MatchChatItem> GetMatchChatForBuyer(Guid buyerId, Guid matchId)
        {
            var messages = await this._messageService.GetMessagesForBuyer(buyerId).Where(b => b.MatchId == matchId).ToListAsync();
            var result = GetMatchChatItem(messages, buyerId);

            return result;
        }

        public async Task<ICollection<MatchChatItem>> GetMatchChatsForSeller(Guid sellerId)
        {
            var messages = await this._messageService.GetMessagesForSeller(sellerId).ToListAsync();
            var result = GetMatchChatItems(messages, sellerId);

            return result;
        }

        public async Task<MatchChatItem> GetMatchChatForSeller(Guid sellerId, Guid matchId)
        {
            var messages = await this._messageService.GetMessagesForSeller(sellerId).Where(b => b.MatchId == matchId).ToListAsync();
            var result = GetMatchChatItem(messages, sellerId);

            return result;
        }

        public async Task<ICollection<ChatMessageItem>> GetChatMessageItems(Guid matchId, string currentUserId)
        {
            var result = new List<ChatMessageItem>();
            var currentCompany = this._companyService.GetCompanyByUserId(currentUserId);
            var userMessages = await this._messageService.GetUserMessagesForMatch(matchId).ToListAsync();
            var oppositeCompany = await GetOppositeCompany(matchId, currentCompany.Id);

            foreach (var message in userMessages)
            {
                var isOwnMessage = message.FromUser.ToString().Equals(currentUserId);
                string userName = GetUserName(message.FromUser.ToString(), currentCompany, oppositeCompany, isOwnMessage);
                var item = new ChatMessageItem()
                {
                    MatchId = matchId,
                    IsUserMessage = true,
                    Id = message.Id,
                    Text = message.Text,
                    CreatedAt = message.CreatedAt,
                    IsOwnMessage = message.FromUser.ToString().Equals(currentUserId),
                    IsRead = message.PendingReadings.Where(o => o.CompanyId.Equals(currentCompany.Id)).Count().Equals(0),
                    UserName = userName
                };

                result.Add(item);
            }

            var systemMessages = await this._messageService.GetSystemMessagesForMatch(matchId).ToListAsync();

            foreach (var message in systemMessages)
            {
                var item = new ChatMessageItem()
                {
                    MatchId = matchId,
                    IsUserMessage = false,
                    Id = message.Id,
                    CreatedAt = message.CreatedAt,
                    Type = message.Type,
                    IsRead = message.PendingReadings.Where(o => o.CompanyId.Equals(currentCompany.Id)).Count().Equals(0)
                };
                result.Add(item);
            }

            return result.OrderByDescending(o => o.CreatedAt).ToList();
        }

        public async Task<ChatMessageItem> AddUserChatMessage(ChatMessageItem chatMessageItem, Guid fromCompanyId, string fromUserId)
        {
            var match = await this._matchService.Get(chatMessageItem.MatchId).SingleAsync();
            var toCompanyId = match.SellerId.Equals(fromCompanyId) ? match.Inquiry.Project.BuyerId : match.SellerId;
            var toUserId = this._companyService.GetCompany(toCompanyId).Users.FirstOrDefault().Id;
            var fromCompany = this._companyService.GetCompany(fromCompanyId);

            chatMessageItem.Id = Guid.NewGuid();


            var pendingReadings = new List<PendingReading>();
            pendingReadings.Add(new PendingReading()
            {
                CompanyId = toCompanyId,
                IsUserMessage = true,
                MessageId = chatMessageItem.Id
            });

            var newUserMessage = new UserMessage()
            {
                Id = chatMessageItem.Id,
                Text = chatMessageItem.Text,
                CreatedAt = DateTime.Now,
                FromUser = new Guid(fromUserId),
                ToUser = new Guid(toUserId),
                MatchId = chatMessageItem.MatchId,
                PendingReadings = pendingReadings
            };
            this._messageService.Add(newUserMessage);

            await this._messageService.SaveAsync();

            var newChatMessageItem = new ChatMessageItem()
            {
                MatchId = newUserMessage.MatchId,
                IsUserMessage = true,
                Id = newUserMessage.Id,
                Text = newUserMessage.Text,
                CreatedAt = newUserMessage.CreatedAt,
                IsOwnMessage = newUserMessage.FromUser.ToString().Equals(fromUserId),
                IsRead = !newUserMessage.PendingReadings.Where(o => o.CompanyId.Equals(fromCompanyId)).Any(),
                UserName = GetUserNameByCompany(fromUserId, fromCompany)
            };

            return newChatMessageItem;
        }

        public async Task<ChatMessageItem> GetChatMessage(Guid messageId, string currentUserId)
        {
            Company currentCompany = this._companyService.GetCompanyByUserId(currentUserId);
            UserMessage message = this._messageService.GetUserMessage(messageId);
            Company oppositeCompany = await GetOppositeCompany(message.MatchId, currentCompany.Id);

            var isOwnMessage = message.FromUser.ToString().Equals(currentUserId);
            string userName = GetUserName(message.FromUser.ToString(), currentCompany, oppositeCompany, isOwnMessage);

            return new ChatMessageItem()
            {
                MatchId = message.MatchId,
                IsUserMessage = true,
                Id = message.Id,
                Text = message.Text,
                CreatedAt = message.CreatedAt,
                IsOwnMessage = message.FromUser.ToString().Equals(currentUserId),
                IsRead = message.PendingReadings.Where(o => o.CompanyId.Equals(currentCompany.Id)).Count().Equals(0),
                UserName = userName
            };
        }

        public async Task<ChatMessageItem> AddSystemChatMessage(ChatMessageItem message, Guid companyId)
        {
            var match = await this._matchService.Get(message.MatchId).SingleAsync();
            var toUserId = this._companyService.GetCompany(companyId).Users.FirstOrDefault().Id;
            message.Id = Guid.NewGuid();
            var pendingReadings = new List<PendingReading>();
            pendingReadings.Add(new PendingReading()
            {
                CompanyId = companyId,
                IsUserMessage = false,
                MessageId = message.Id
            });

            var input = new SystemMessage()
            {
                Id = message.Id,
                CreatedAt = DateTime.Now,
                MatchId = message.MatchId,
                Type = message.Type ?? MessageType.newMatch,
                PendingReadings = pendingReadings
            };
            this._messageService.Add(input);

            await this._messageService.SaveAsync();

            return message;
        }

        public async Task SetMessageReadForUser(Guid id, string userId)
        {
            var company = this._companyService.GetCompanyByUserId(userId);
            await this._messageService.MarkAsRead(id, company.Id);
            await this._messageService.SaveAsync();
        }

        private async Task<Company> GetOppositeCompany(Guid matchId, Guid companyId)
        {
            Company result;
            var match = await this._matchService.Get(matchId).SingleAsync();

            if (match.SellerId.Equals(companyId))
            {
                result = this._companyService.GetCompany(match.Inquiry.Project.BuyerId);
            }
            else
            {
                result = this._companyService.GetCompany(match.SellerId);
            }

            return result;
        }

        private static string GetUserName(string userId, Company currentCompany, Company oppositeCompany, bool isOwnMessage)
        {
            string result;
            if (isOwnMessage)
            {
                result = GetUserNameByCompany(userId, currentCompany);
            }
            else
            {
                result = GetUserNameByCompany(userId, oppositeCompany);
            }

            return result;
        }

        private static string GetUserNameByCompany(string userId, Company company)
        {
            var username = string.Empty;
            var user = company.Users.Where(o => o.Id.Equals(userId)).SingleOrDefault();

            if (user != null)
            {
                username = string.Format("{0} {1}", user.Firstname, user.Lastname);
            }

            return username;
        }

        /// <summary>
        /// TODO: optimieren, lädt viel zu viel und viel zu lange
        /// </summary>
        private static ICollection<MatchChatItem> GetMatchChatItems(List<Message> messages, Guid companyId)
        {
            var result = new List<MatchChatItem>();
            var matchids = messages.Select(o => o.MatchId).Distinct();

            foreach (var matchid in matchids)
            {
                var matchMessages = messages.Where(o => o.MatchId.Equals(matchid)).ToList();
                var project = matchMessages.Select(o => o.Match.Inquiry.Project).FirstOrDefault();
                var seller = matchMessages.Select(o => o.Match.Seller).FirstOrDefault();
                var newestOffer = matchMessages.SelectMany(o => o.Match.Offers).OrderByDescending(o => o.CreatedAt).FirstOrDefault();
                int unreadcount = GetUnReadCount(companyId, matchMessages);
                var item = new MatchChatItem()
                {
                    MatchId = matchid,
                    SellerId = seller.Id,
                    BuyerId = project.Buyer.Id,
                    PostalCode = project.PostalCode,
                    City = project.City,
                    Street = project.Street,
                    ProjectName = project.ProjectName,
                    SellerName = seller.Name,
                    BuyerName = project.Buyer.Name,
                    SellerPostalCode = seller.PostalCode,
                    SellerCity = seller.City,
                    UnreadCount = unreadcount,
                    OfferNumber = newestOffer?.Number.ToString(),
                    OfferDate = newestOffer?.CreatedAt,
                    NewestMessageDate = messages.Where(o => o.MatchId.Equals(matchid)).Max(o => o.CreatedAt)                    
                };
                result.Add(item);
            }

            return result;
        }

        private static MatchChatItem GetMatchChatItem(List<Message> messages, Guid companyId)
        {
            return GetMatchChatItems(messages, companyId).FirstOrDefault();
        }

        private static int GetUnReadCount(Guid companyId, List<Message> matchMessages)
        {
            var unreadcount = 0;

            foreach (var message in matchMessages)
            {
                unreadcount += message.PendingReadings.Where(o => o.CompanyId.Equals(companyId)).Count();
            }

            return unreadcount;
        }
    }
}