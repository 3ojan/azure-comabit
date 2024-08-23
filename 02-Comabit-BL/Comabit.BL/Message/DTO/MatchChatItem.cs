// <copyright file="MatchChatItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Message.DTO
{
    public class MatchChatItem
    {
        public Guid MatchId { get; set; }

        public Guid BuyerId { get; set; }

        public Guid SellerId { get; set; }

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

        public int UnreadCount { get; set; }

        public DateTime NewestMessageDate { get; internal set; }

        public ChatMessageItem NewestMessage { get; internal set; }
    }
}
