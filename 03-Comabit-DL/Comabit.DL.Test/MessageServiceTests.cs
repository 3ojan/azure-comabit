// <copyright file="CompanyServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Company;
using Comabit.DL.Data.Match;
using Comabit.DL.Data.Portfolio;
using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.DL.Test
{
    public class MessageServiceTests : BaseServiceTests
    {
        private MessageService _messageService;
        private Guid _sellerCompanyId = new Guid("311e53fa-4a45-44ae-aa07-39b77ccdfc90");
        private Guid _buyerCompanyId = new Guid("91cf7604-95eb-4691-b701-ab437a6e003a");

        [SetUp]
        public void Setup()
        {
            this._messageService = new MessageService(this.UnitOfWork);
        }

        [Test]
        public void GetAllMessages()
        {
            var result = this._messageService.GetAll().ToList();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddUserMessageTest()
        {
            var id = Guid.NewGuid();
            var pendingReadings = new List<PendingReading>();
            pendingReadings.Add(new PendingReading()
            {
                CompanyId = new Guid("d79d00e7-3cfa-4465-994d-17485dc1cdac"),
                IsUserMessage = true,
                MessageId = id
            });
            var input = new UserMessage()
            {
                Id = id,
                Text = "Das ist noch mal ein Text ",
                CreatedAt = DateTime.Now,
                FromUser = new Guid("05775d92-8720-4e04-ae1b-bbe922d0cfb8"),
                ToUser = new Guid("2bde8af1-0855-4bc6-aec5-37e095c7509c"),
                MatchId = new Guid("db3fd6fc-1a29-4403-9230-f97665d8b553"),
                PendingReadings = pendingReadings
            };

            this._messageService.Add(input);
            await this._messageService.SaveAsync();
        }

        [Test]
        public async Task GetMessagesForSellerTest()
        {
            var sellerId = new Guid("d79d00e7-3cfa-4465-994d-17485dc1cdac");
            var messages = this._messageService.GetMessagesForSeller(sellerId);

            Assert.IsNotNull(messages);
        }
    }
}