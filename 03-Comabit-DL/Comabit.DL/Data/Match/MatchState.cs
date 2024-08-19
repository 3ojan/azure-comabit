// <copyright file="Matchestate.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using System.ComponentModel;

    public enum MatchState
    {
        [Description("offen")]
        pending,

        [Description("abgelehnt")]
        revoked,

        [Description("beauftragt")]
        ordered,

        [Description("nachbessern")]
        renew,

        [Description("akzeptiert")]
        accepted,

        [Description("angeboten")]
        offered,
    }
}