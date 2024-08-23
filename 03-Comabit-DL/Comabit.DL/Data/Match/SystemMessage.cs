// <copyright file="UserMessage.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SystemMessage : Message
    {
        public MessageType Type { get; set; }
    }
}