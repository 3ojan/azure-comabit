// <copyright file="ChatMessageViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Message
{
    using Comabit.DL.Data.Match;
    using System;

    public class ChatMessageViewModel
    {
        public Guid Id { get; set; }

        public Guid MatchId { get; set; }

        public string Text { get; set; }

        public bool IsRead { get; set; }

        public bool IsOwnMessage { get; set; }

        public bool IsUserMessage { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Date { get => CreatedAt.ToString("dd.MM."); }

        public string Hour { get => CreatedAt.ToString("HH:mm"); }

        public bool DateIsToday { get => CreatedAt.Date == DateTime.Today; }

        public MessageType? Type { get; set; }

        public string TypeDescription
        {
            get
            {
                switch (Type)
                {
                    case MessageType.newMatch:
                        return "neuer Match wurde gefunden";

                    case MessageType.ordered:
                        return "Angebot wurde angenommen.";

                    case MessageType.renew:
                        return "Nachbesserung wurde angefordert.";

                    case MessageType.revoked:
                        return "Angebot wurde abgelehnt.";

                    default:
                        return "";
                }
            }
        }
    }
}