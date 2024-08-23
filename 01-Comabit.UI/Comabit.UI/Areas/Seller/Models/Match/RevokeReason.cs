// <copyright file="RevokeReason.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace Comabit.UI.Areas.Seller.Models.Match
{
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