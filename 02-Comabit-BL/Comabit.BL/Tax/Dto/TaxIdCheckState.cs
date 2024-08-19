// <copyright file="TaxIdCheckState.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Tax.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TaxIdCheckState
    {
        public string Code
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public TaxIdCheckStateType Type
        {
            get;
            set;
        }

        public TaxIdCheckState(string code, string message, TaxIdCheckStateType responseType)
        {
            Code = code;
            Message = message;
            Type = responseType;
        }
    }
}