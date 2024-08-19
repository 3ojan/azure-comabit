// <copyright file="InquirySellerExclusionItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Inquiry.Dto
{
    using Comabit.BL.Company.Dto;
    using System;

    public class InquirySellerExclusionItem
    {
        public Guid InquiryId { get; set; }

        public InquiryItem Inquiry { get; set; }

        public Guid SellerId { get; set; }

        public SellerItem Seller { get; set; }
    }
}