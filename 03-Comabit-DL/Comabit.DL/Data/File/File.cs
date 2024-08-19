// <copyright file="File.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.File
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class File
    {
        [Key]
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public int Size { get; set; }

        public FileData FileData { get; set; }
    }
}