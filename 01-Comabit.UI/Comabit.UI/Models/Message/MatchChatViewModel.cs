// <copyright file="MatchChatViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Message
{
    using System;
    using System.Collections.Generic;

    public class MatchChatViewModel
    {
        public Guid MatchId { get; set; }

        public Guid SellerId { get; set; }

        public Guid BuyerId { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string ProjectName { get; set; }
        
        public string SellerName { get; set; }

        public string BuyerName { get; set; }

        public string SellerPostalCode { get; set; }

        public string SellerCity { get; set; }

        public string OfferNumber { get; set; }

        public DateTime? OfferDate { get; set; }

        public string OfferDateFormatted { get => OfferDate.HasValue ? OfferDate.Value.ToString("dd.MM. HH:mm") : ""; }

        public int UnreadCount { get; set; }

        public DateTime NewestMessageDate { get; internal set; }

        public ChatMessageViewModel NewestMessage { get; set; }
    }
}