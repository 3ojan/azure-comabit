// <copyright file="IUnitOfWork.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IAsyncDisposable
    {
        ApplicationDbContext DbContext
        {
            get;
        }

        [Obsolete("Please use SaveAsync method instead.")]
        int Save();

        Task<int> SaveAsync();
    }
}