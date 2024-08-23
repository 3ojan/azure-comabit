// <copyright file="LogViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Match
{
    using System;

    public class LogViewModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? InquiryId { get; set; }

        public InquiryViewModel Inquiry { get; set; }

        public Guid? BuyerId { get; set; }

        public Guid? SellerId { get; set; }

        public Guid? OfferId { get; set; }

        public Guid? MatchId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}