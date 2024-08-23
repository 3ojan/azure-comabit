// <copyright file="MatchDetailViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Seller.Models.Match
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateOfferViewModel
    {
        [Required]
        public Guid MatchId { get; set; }

        public string Message { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}