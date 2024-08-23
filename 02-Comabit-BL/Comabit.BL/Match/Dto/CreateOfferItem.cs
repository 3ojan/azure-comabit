// <copyright file="CreateOfferItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Match.Dto
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateOfferItem
    {
        public Guid MatchId { get; set; }

        public string Message { get; set; }

        public IFormFile File { get; set; }
    }
}