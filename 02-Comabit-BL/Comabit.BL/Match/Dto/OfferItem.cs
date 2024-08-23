// <copyright file="OfferItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Match.Dto
{
    using Comabit.BL.File.Dto;
    using Comabit.DL.Data.Match;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OfferItem
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string BuyerNote { get; set; }

        public string Text { get; set; }

        public OfferState State { get; set; }

        public FileItem File { get; set; }

        public Guid FileId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public MatchItem Match { get; set; }

        public Guid MatchId { get; set; }
    }
}