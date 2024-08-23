// <copyright file="ApplicationRole.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name) : base(name)
        {
            this.Permissions = new HashSet<ApplicationPermission>();
        }

        public string Description { get; set; }

        public virtual ICollection<ApplicationPermission> Permissions { get; set; }
    }
}
