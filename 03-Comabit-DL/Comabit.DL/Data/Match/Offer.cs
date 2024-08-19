// <copyright file="Offer.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using Comabit.DL.Data.File;
    using Comabit.DL.Data.Inquiry;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Offer
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public string BuyerNote { get; set; }

        public OfferState State { get; set; }

        public string Text { get; set; }

        public OfferFile File { get; set; }

        [ForeignKey("File")]
        public Guid FileId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Match Match { get; set; }

        public Guid MatchId { get; set; }
    }
}