// <copyright file="FileDataItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.File.Dto
{
    using System;

    public class FileDataItem
    {
        public Guid FileId { get; set; }

        public byte[] Data { get; set; }
    }
}