// <copyright file="PortfolioService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Inquiry;
using System;

namespace Comabit.DL.Interfaces
{
    public interface IInquirySellerExclusionService
    {
        void AddExclusion(Guid inquiryId, Guid sellerId);

        void RemoveExclusion(InquirySellerExclusion exclusion);
    }
}