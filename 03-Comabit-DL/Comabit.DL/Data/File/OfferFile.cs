// <copyright file="OfferFile.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.File
{
    using Comabit.DL.Data.Match;
    using System;

    public class OfferFile : File
    {
        public Offer Offer { get; set; }

        public Guid OfferId { get; set; }
    }
}