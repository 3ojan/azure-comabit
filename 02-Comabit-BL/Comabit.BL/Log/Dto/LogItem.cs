using Comabit.BL.Company.Dto;
using Comabit.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Inquiry.Dto
{
    public class LogItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? InquiryId { get; set; }

        public InquiryItem Inquiry { get; set; }

        public Guid? BuyerId { get; set; }

        public Guid? SellerId { get; set; }

        public Guid? OfferId { get; set; }

        public Guid? MatchId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}