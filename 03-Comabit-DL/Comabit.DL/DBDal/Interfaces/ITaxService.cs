// <copyright file="ITaxService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Geo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITaxService
    {
        Task<Dictionary<string, string>> CheckIdNumber(string taxIdNumber);

        Task<Dictionary<string, string>> CheckIdNumberQualified(string taxIdNumber, string companyName, string city, string postalCode = "", string street = "");
    }
}