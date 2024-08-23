// <copyright file="InquirySellerExclusion.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Inquiry
{
    using Comabit.DL.Data.Company;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InquirySellerExclusion
    {
        public Guid InquiryId { get; set; }

        public Inquiry Inquiry { get; set; }

        public Guid SellerId { get; set; }

        public Seller Seller { get; set; }
    }
}