// <copyright file="RolesPermissionsListViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.DL;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RolesPermissionsListViewModel
    {
        public List<RoleListViewModel> Roles { get; set; }

        public List<ApplicationPermission> Permissions { get; set; }

        public List<AuthRolesPermissionsViewModel> RolesPermissions { get; set; }
    }
}