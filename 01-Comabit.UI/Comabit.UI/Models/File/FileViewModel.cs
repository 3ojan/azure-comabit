using System;

namespace Comabit.UI.Models.File
{
    public class FileViewModel
    {
        public Guid Id { get; set; }

        public Guid InquiryId { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public int Size { get; set; }

        public bool Delete { get; set; }
    }
}