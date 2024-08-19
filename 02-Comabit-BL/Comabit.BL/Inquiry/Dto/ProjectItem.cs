using Comabit.BL.Company.Dto;
using Comabit.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Inquiry.Dto
{
    public class ProjectItem
    {
        public Guid Id { get; set; }

        public Guid BuyerId { get; set; }

        public BuyerItem Buyer { get; set; }

        public string ProjectName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedByUserId { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string ContactEmail { get; set; }

        public string ContactClerk { get; set; }

        public bool IsActive { get; set; }

        public ICollection<InquiryItem> Inquiries { get; set; }
    }
}