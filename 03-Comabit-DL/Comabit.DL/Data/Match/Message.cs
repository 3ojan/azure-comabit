// <copyright file="Message.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Match Match { get; set; }

        public Guid MatchId { get; set; }

        public ICollection<PendingReading> PendingReadings { get; set; }
    }
}