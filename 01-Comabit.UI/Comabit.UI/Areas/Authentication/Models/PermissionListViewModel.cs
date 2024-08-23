// <copyright file="PermissionListViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using System.Collections.Generic;

    public class PermissionListViewModel
    {
        public List<PermissionViewModel> Permissions { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public PermissionListViewModel()
        {
            Roles = new List<RoleViewModel>();
        }
    }
}