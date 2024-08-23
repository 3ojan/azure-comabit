// <copyright file="PlacingState.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Inquiry
{
    using System.ComponentModel;

    public enum PlacingState
    {
        [Description("offen")]
        Open,

        [Description("teil beauftragt")]
        PartialPlaced,

        [Description("komplett beauftragt")]
        FullPlaced,

        [Description("nicht beauftragt")]
        NotPlaced,
    }
}