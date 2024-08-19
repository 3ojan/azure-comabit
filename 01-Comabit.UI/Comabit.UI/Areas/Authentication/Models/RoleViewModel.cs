// <copyright file="RoleViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.DL;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Bezeichnung")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<PermissionViewModel> AssignedPermissions { get; set; }

        public bool HasPermission(long permissionId)
        {
            return AssignedPermissions.Any(ap => ap.Id == permissionId);
        }
    }
}