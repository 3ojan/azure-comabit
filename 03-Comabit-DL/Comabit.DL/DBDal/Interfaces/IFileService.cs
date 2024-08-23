// <copyright file="IPortfolioService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.File;
    using System;
    using System.Linq;

    public interface IFileService : IAsyncDisposable
    {
        IQueryable<File> Get(Guid id);

        void Delete(Guid id);
    }
}