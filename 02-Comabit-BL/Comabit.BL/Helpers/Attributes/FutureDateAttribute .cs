// <copyright file="FutureDateAttribute.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Helpers.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value == null || (DateTime)value > DateTime.Now;
        }
    }
}