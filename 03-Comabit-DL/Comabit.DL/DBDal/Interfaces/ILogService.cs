// <copyright file="ILogService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Log;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ILogService : IAsyncDisposable
    {
        IQueryable<Log> GetAll();

        void CreateLog(string message, Inquiry inquiry);

        void Add(Log log);

        Task<int> SaveAsync();
    }
}