using Comabit.DL.Data.Inquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.File
{
    public class InquiryFile : File
    {
        public Guid InquiryId { get; set; }

        public Inquiry.Inquiry Inquiry { get; set; }
    }
}