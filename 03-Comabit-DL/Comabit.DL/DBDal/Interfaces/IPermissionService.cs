// <copyright file="IPermissionService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPermissionService : IAsyncDisposable
    {
        IQueryable<ApplicationPermission> GetAll();

        IEnumerable<ApplicationPermission> GetPermissionForRole(string roleId);

        ApplicationPermission GetPermission(Guid guid);

        void AddPermission(ApplicationPermission permission);

        Task<int> SaveAsync();
    }
}
