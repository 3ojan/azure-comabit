// <copyright file="IMessageService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Match;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        IQueryable<Message> GetMessages(Guid matchId);

        IQueryable<Message> GetMessagesForBuyer(Guid buyerId);

        IQueryable<Message> GetMessagesForSeller(Guid sellerId);

        IQueryable<UserMessage> GetUserMessagesForMatch(Guid matchId);

        UserMessage GetUserMessage(Guid messageId);

        IQueryable<SystemMessage> GetSystemMessagesForMatch(Guid matchId);

        void Add(UserMessage message);

        void Add(SystemMessage message);

        Task MarkAsRead(Guid messageId, Guid userId);

        Task MarkAsRead(IEnumerable<Guid> messageIds, Guid userId);

        int GetUnreadUserMessageCount(Guid userId);

        int GetUnreadSystemMessageCount(Guid userId);

        IQueryable<Message> GetAll();

        Task<int> SaveAsync();
    }
}