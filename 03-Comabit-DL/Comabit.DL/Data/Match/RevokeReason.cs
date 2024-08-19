// <copyright file="RevokeReason.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Match
{
    using System.ComponentModel;

    public enum RevokeReason
    {
        [Description("kein interesse")]
        not_interested,

        [Description("nicht zuständig")]
        not_responsible,

        [Description("sonstiges")]
        other
    }
}