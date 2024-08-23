// <copyright file="ProjectDetailViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Match
{
    using Comabit.UI.Models.Company;
    using System;
    using System.Collections.Generic;

    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        public Guid BuyerId { get; set; }

        public BuyerViewModel Buyer { get; set; }

        public string ProjectName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByUserId { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string ContactEmail { get; set; }

        public string ContactClerk { get; set; }

        public bool IsActive { get; set; }

        public ICollection<InquiryViewModel> Inquiries { get; set; }
    }
}