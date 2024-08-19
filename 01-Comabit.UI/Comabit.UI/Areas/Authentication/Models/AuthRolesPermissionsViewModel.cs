// <copyright file="AuthRolesPermissionsViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using System.Collections.Generic;

    public class AuthRolesPermissionsViewModel
    {
        public string RoleId { get; set; }

        public List<int> PermissionIds { get; set; }
    }
}