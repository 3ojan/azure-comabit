// <copyright file="NotificationType.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using System.ComponentModel;

    public enum MessageType
    {
        [Description("abgelehnt")]
        revoked,

        [Description("beauftragt")]
        ordered,

        [Description("nachbessern")]
        renew,

        [Description("neuer Match")]
        newMatch,
    }
}