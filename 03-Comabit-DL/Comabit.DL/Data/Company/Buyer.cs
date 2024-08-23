// <copyright file="BuyerCompany.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Company
{
    using Comabit.DL.Data.Inquiry;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Buyer : Company
    {
        public ICollection<Project> Projects { get; set; }
    }
}