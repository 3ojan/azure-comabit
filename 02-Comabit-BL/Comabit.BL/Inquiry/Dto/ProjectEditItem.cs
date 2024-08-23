using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Inquiry.Dto
{
    public class ProjectEditItem
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedByUserId { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string ContactEmail { get; set; }

        public string ContactClerk { get; set; }

        public bool IsActive { get; set; }
    }
}