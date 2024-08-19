// <copyright file="MatchDetailViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Seller.Models.Match
{
    using Comabit.UI.Models.Match;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeleteViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public RevokeReason RevokeReason { get; set; }

        //[RequiredIf("RevokeReason == RevokeReason.other")]
        public string RevokeReasonText { get; set; }

        public InquiryViewModel Inquiry { get; set; }
    }
}