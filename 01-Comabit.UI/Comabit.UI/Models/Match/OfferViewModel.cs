// <copyright file="OfferViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Match
{
    using System;
    using Comabit.DL.Data.Match;
    using Comabit.UI.Models.File;

    public class OfferViewModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string BuyerNote { get; set; }

        public string Text { get; set; }

        public FileViewModel File { get; set; }

        public Guid FileId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public OfferState State { get; set; }

        public MatchViewModel Match { get; set; }
    }
}