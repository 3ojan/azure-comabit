// <copyright file="LogManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Log
{
    using Comabit.BL.Inquiry.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LogManager : BaseManager
    {
        private readonly ILogService _logService;

        private readonly IInquiryService _inquiryService;

        public LogManager(ILogService logService, IInquiryService inquiryService)
        {
            this._logService = logService;
            this._inquiryService = inquiryService;
        }

        public async Task Add(LogItem log)
        {
            _logService.Add(this.Mapper.Map<DL.Data.Log.Log>(log));
            await _logService.SaveAsync();
        }

        public async Task<ICollection<LogItem>> Get(int amount)
        {
            ICollection<LogItem> logItems = this.Mapper.Map<ICollection<LogItem>>(await _logService.GetAll().Take(amount).OrderByDescending(l => l.CreatedAt).ToListAsync());

            ICollection<Guid> inquiryIds = logItems.Where(l => l.InquiryId.HasValue).Select(l => l.InquiryId.Value).ToList();
            ICollection<InquiryItem> inquiries = this.Mapper.Map<ICollection<InquiryItem>>(await this._inquiryService.GetAll().Where(i => inquiryIds.Contains(i.Id)).ToListAsync());

            foreach (LogItem logItem in logItems.Where(l => l.InquiryId.HasValue))
            { 
                logItem.Inquiry = inquiries.Where(i => i.Id == logItem.InquiryId).FirstOrDefault();
            }

            return logItems;
        }
    }
}