using Comabit.DL.Data.Inquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Inquiry.Dto
{
    public class FileItem : Comabit.BL.File.Dto.FileItem
    {
        public Guid InquiryId { get; set; }

        public InquiryItem Inquiry { get; set; }
    }
}