using System;

namespace Comabit.UI.Models.File
{
    public class FileDataViewModel
    {
        public Guid FileId { get; set; }

        public byte[] Data { get; set; }
    }
}