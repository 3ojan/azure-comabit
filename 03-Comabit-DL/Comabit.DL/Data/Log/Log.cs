// <copyright file="Log.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Log
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Log
    {
        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? InquiryId { get; set; }

        public Guid? BuyerId { get; set; }

        public Guid? SellerId { get; set; }

        public Guid? OfferId { get; set; }

        public Guid? MatchId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}