// <copyright file="Match.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Inquiry;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Match
    {
        [Key]
        public Guid Id { get; set; }

        public double Score { get; set; }

        public MatchState State { get; set; }

        public RevokeReason? RevokeReason { get; set; }

        public string RevokeReasonText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Inquiry Inquiry { get; set; }

        public Guid InquiryId { get; set; }

        public Seller Seller { get; set; }

        public Guid SellerId { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Match()
        {
            this.Offers = new HashSet<Offer>();
            this.Messages = new HashSet<Message>();
        }
    }
}