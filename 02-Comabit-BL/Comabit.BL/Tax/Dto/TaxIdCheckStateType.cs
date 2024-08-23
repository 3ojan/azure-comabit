// <copyright file="TaxIdCheckStateType.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Tax.Dto
{
    using System.ComponentModel;

    public enum TaxIdCheckStateType
    {
        Valid = 1,
        Invalid = 2,
        ServiceUnavailable = 3,
        CheckImpossible = 4,
        IdSyntaxError = 5,
        RequestIdInvalid = 6,
        Unknown = 7,
    }
}