// <copyright file="CompanyModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Company.Dto
{
    using Comabit.BL.Porfolio.Dto;
    using Comabit.DL;
    using Comabit.DL.Data.Company;
    using System;
    using System.Collections.Generic;

    public class CompanyItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string BusinessTaxId { get; set; }

        public string UstId { get; set; }

        public CompanyState State { get; set; }

        public string Role { get; set; }

        public string MainUserId { get; set; }

        public ApplicationUser MainUser { get; set; }

        public IList<ApplicationUser> Users { get; set; }
    }
}