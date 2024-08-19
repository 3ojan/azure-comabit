// <copyright file="UserMessage.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserMessage : Message
    {
        public Guid FromUser { get; set; }

        public Guid? ToUser { get; set; }

        public string Text { get; set; }
    }
}