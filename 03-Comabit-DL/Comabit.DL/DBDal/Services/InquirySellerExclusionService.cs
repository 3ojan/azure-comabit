// <copyright file="InquirySellerExclusionService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.File;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Portfolio;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InquirySellerExclusionService : IInquirySellerExclusionService
    {
        private IUnitOfWork unitOfWork;
        private readonly IGenericRepository<InquirySellerExclusion> _inquirySellerExclusionRepository;
        
        public InquirySellerExclusionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this._inquirySellerExclusionRepository = new GenericRepository<InquirySellerExclusion>(this.unitOfWork.DbContext);
        }

        public void AddExclusion(Guid inquiryId, Guid sellerId)
        {
            this._inquirySellerExclusionRepository.Add(new InquirySellerExclusion() { InquiryId = inquiryId, SellerId = sellerId });
        }

        public void RemoveExclusion(InquirySellerExclusion exclusion)
        {
            this._inquirySellerExclusionRepository.Delete(exclusion);
        }

        public async Task<int> SaveAsync()
        {
            return await unitOfWork.SaveAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}