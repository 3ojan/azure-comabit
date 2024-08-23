// <copyright file="MessageManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Company;
using Comabit.BL.Match;
using Comabit.BL.Message;
using Comabit.DL.Interfaces;
using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.BL.Test
{
    public class MessageManagerTest : BaseManagerTests
    {
        private MessageManager _messageManager;

        private IMessageService _messageService;
        private ICompanyService _companyService;
        private IMatchService _matchService;

        [SetUp]
        public void Setup()
        {
            this._messageService = new MessageService(this.UnitOfWork);
            this._companyService = new CompanyService(this.UnitOfWork);
            this._matchService = new MatchService(this.UnitOfWork);
            this._messageManager = new MessageManager(this._messageService, this._companyService, this._matchService);
        }
        
        [Test]
        public async ValueTask GetMatchChatsForBuyerItemTest()
        {
            var buyerId = new Guid("2bde8af1-0855-4bc6-aec5-37e095c7509c");
            var result = await this._messageManager.GetMatchChatsForBuyer(buyerId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async ValueTask GetMatchChatsForSellerTest()
        {
            var sellerId = new Guid("d79d00e7-3cfa-4465-994d-17485dc1cdac");
            var result = await this._messageManager.GetMatchChatsForSeller(sellerId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async ValueTask AddMessageTest()
        {
            var sellerId = new Guid("d79d00e7-3cfa-4465-994d-17485dc1cdac");
            var userId = "05775d92-8720-4e04-ae1b-bbe922d0cfb8";
            var matchId = new Guid("db3fd6fc-1a29-4403-9230-f97665d8b553");
            var message = new Message.DTO.ChatMessageItem()
            {
                MatchId = matchId,
                Text = "add message by AddMessageTest",
                CreatedAt = DateTime.Now,
                IsUserMessage = true,
                UserName = "Moritz Verkäufermann",
                IsRead = false,
                IsOwnMessage = true
            };
            var result = await this._messageManager.AddUserChatMessage(message, sellerId, userId);

            Assert.IsNotNull(result);
        }
    }
}