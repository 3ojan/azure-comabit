// <copyright file="Company.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Company
{
    using Comabit.DL.Data.Match;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string UstId { get; set; }

        public string BusinessTaxId { get; set; }

        public bool Confirmed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string MainUserId { get; set; }

        public CompanyState State { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<PendingReading> PendingReadings { get; set; }
    }
}