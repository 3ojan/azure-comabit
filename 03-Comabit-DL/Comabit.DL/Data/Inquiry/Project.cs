using Comabit.DL.Data.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.Inquiry
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BuyerId { get; set; }

        public Buyer Buyer { get; set; }

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

        public ICollection<Inquiry> Inquiries { get; set; }

        public Project()
        {
            Inquiries = new HashSet<Inquiry>();
        }
    }
}