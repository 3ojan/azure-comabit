using Comabit.DL.Data.Company;
using Comabit.DL.Data.Inquiry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.Match
{
    public class PendingReading
    {
        [Column(Order = 1)]
        public Guid MessageId { get; set; }

        [Column(Order = 2)]
        public Guid CompanyId { get; set; }

        public bool IsUserMessage { get; set; }

        public Company.Company Company { get; set; }

        public Message Message { get; set; }
    }
}