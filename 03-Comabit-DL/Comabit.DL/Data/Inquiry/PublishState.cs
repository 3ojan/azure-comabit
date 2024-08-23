// <copyright file="PublishState.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Inquiry
{
    using System.ComponentModel;

    public enum PublishState
    {
        [Description("Entwurf")]
        draft,

        [Description("Veröffentlichen")]
        publish,

        [Description("Veröffentlichen am")]
        publishAt,
    }
}