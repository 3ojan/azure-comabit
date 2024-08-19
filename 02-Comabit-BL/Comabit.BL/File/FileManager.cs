// <copyright file="FileManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.File
{
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Comabit.DL.Data.File;
    using Comabit.BL.File.Dto;

    public class FileManager : BaseManager
    {
        private IFileService _fileService;

        public FileManager(IFileService fileService)
        {
            this._fileService = fileService;
        }

        public async Task<FileItem> Get(Guid id)
        {
            File fileData = await this._fileService.Get(id).FirstOrDefaultAsync();

            return this.Mapper.Map<FileItem>(fileData);
        }
    }
}