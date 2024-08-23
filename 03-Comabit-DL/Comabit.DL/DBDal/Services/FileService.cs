// <copyright file="FileService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.File;
    using Comabit.DL.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class FileService : IFileService
    {
        private IUnitOfWork unitOfWork;
        private readonly IGenericRepository<File> _fileRepository;

        public FileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this._fileRepository = new GenericRepository<File>(this.unitOfWork.DbContext);
        }

        public IQueryable<File> Get(Guid id)
        {
            return this._fileRepository.GetAll(includeProperties: "FileData").Where(f => f.Id == id);
        }

        public void Delete(Guid id)
        {
            File file = this._fileRepository.Get(f => f.Id == id).FirstOrDefault();

            if (file != null)
            {
                this._fileRepository.Delete(file);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}