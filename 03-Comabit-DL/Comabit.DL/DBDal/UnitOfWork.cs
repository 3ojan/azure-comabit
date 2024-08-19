// <copyright file="UnitOfWork.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL
{
    using Comabit;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private ApplicationDbContext context;

        public ApplicationDbContext DbContext => this.context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            this.context.ChangeTracker.LazyLoadingEnabled = false;
            // TODO Lazyloading ist immer noch aktiv.  this.db.Configuration.LazyLoadingEnabled = false;
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (this.context != null)
            {
                this.context = null;
            }

            await Task.Yield();

            
            GC.SuppressFinalize(this);
        }
    }
}