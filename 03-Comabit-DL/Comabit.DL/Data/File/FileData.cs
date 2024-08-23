// <copyright file="FileData.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.File
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FileData
    {
        public File File { get; set; }

        [Key]
        public Guid FileId { get; set; }

        public byte[] Data { get; set; }
    }
}