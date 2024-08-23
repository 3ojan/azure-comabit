// <copyright file="PermissionService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PermissionService : IPermissionService
    {
        private IUnitOfWork unitOfWork;

        private IGenericRepository<ApplicationPermission> permissionRepository;

        private IGenericRepository<ApplicationRole> roleRepository;

        public PermissionService(IUnitOfWork unitOfWork, IGenericRepository<ApplicationPermission> permissionRepository, IGenericRepository<ApplicationRole> roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.permissionRepository = permissionRepository;
            this.roleRepository = roleRepository;
        }

        public IQueryable<ApplicationPermission> GetAll()
        {
            return this.permissionRepository.GetAll();
        }

        public IEnumerable<ApplicationPermission> GetPermissionForRole(string roleId)
        {
            return this.roleRepository.Get(r => r.Id == roleId, includeProperties: "Permissions").SelectMany(r => r.Permissions);
        }

        public ApplicationPermission GetPermission(Guid guid)
        {
            return this.permissionRepository.Get(p => p.Guid == guid, includeProperties: "Roles").FirstOrDefault();
        }

        public void AddPermission(ApplicationPermission permission)
        {
            this.permissionRepository.Add(permission);
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
