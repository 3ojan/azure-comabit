// <copyright file="PermissionViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.DL;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PermissionViewModel
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Required]
        public string Value { get; set; }

        public string Description { get; set; }

        public string GroupName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public PermissionViewModel()
        {
        }

        public PermissionViewModel(ApplicationPermission permission)
        {
            Id = permission.ApplicationPermissionId;
            Guid = permission.Guid;
            Value = permission.Value;
            Description = permission.Description;
            GroupName = permission.GroupName;
        }
    }
}