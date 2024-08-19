// <copyright file="ChatMessageItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Match;
using System;
using System.Collections.Generic;

namespace Comabit.BL.Message.DTO
{
    public class ChatMessageItem
    {
        public Guid Id { get; set; }

        public Guid MatchId { get; set; }

        public string Text { get; set; }

        public bool IsRead { get; set; }

        public bool IsOwnMessage { get; set; }

        public bool IsUserMessage { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }

        public MessageType? Type { get; set; }
    }
}
