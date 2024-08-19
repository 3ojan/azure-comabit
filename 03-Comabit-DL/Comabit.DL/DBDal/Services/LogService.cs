// <copyright file="LogService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Log;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;

    public class LogService : ILogService
    {
        private IUnitOfWork unitOfWork;

        private IGenericRepository<Log> _logRepository;

        public LogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            
            this._logRepository = new GenericRepository<Log>(this.unitOfWork.DbContext);
        }

        public IQueryable<Log> GetAll()
        {
            return this._logRepository.GetAll();
        }

        public void CreateLog(string message, Inquiry inquiry)
        {
            Log log = new Log()
            {
                Id = Guid.NewGuid(),
                BuyerId = inquiry?.Project?.BuyerId,
                Description = message,
                ProjectId = inquiry?.ProjectId,
                InquiryId = inquiry?.Id,
                CreatedAt = DateTime.Now,
            };

            Add(log);
        }

        public void Add(Log log)
        {
            this._logRepository.Add(log);
        }

        public async Task<int> SaveAsync()
        {
            return await unitOfWork.SaveAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}