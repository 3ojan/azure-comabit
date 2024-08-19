// <copyright file="MatchItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Match.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Comabit.BL.Company.Dto;
    using Comabit.BL.Inquiry.Dto;
    using Comabit.DL.Data.Match;

    public class MatchItem
    {
        public Guid Id { get; set; }

        public Guid SellerId { get; set; }

        public double Score { get; set; }

        public int State { get; set; }

        public int RevokeReason { get; set; }

        public string RevokeReasonText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public InquiryItem Inquiry { get; set; }

        public Guid InquiryId { get; set; }

        public SellerItem Seller { get; set; }

        public ICollection<OfferItem> Offers { get; set; }

        public ICollection<MessageItem> Messages { get; set; }
    }
}