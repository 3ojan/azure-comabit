// <copyright file="InquirySellerExclusionViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Buyer.Models.Inquiry
{
    using Comabit.UI.Models.Company;
    using Comabit.UI.Models.Match;
    using System;

    public class InquirySellerExclusionViewModel
    {
        public Guid InquiryId { get; set; }

        public InquiryViewModel Inquiry { get; set; }

        public Guid SellerId { get; set; }

        public SellerViewModel Seller { get; set; }

        public bool Checked { get; set; }
    }
}