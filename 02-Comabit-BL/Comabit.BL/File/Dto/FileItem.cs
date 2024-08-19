// <copyright file="FileItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.File.Dto
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FileItem
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public int Size { get; set; }

        public FileDataItem FileData { get; set; }

        public bool Delete { get; set; }
    }
}