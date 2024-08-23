namespace Comabit.DL
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    public class ApplicationPermission
    {
        public ApplicationPermission()
        {
            this.Roles = new HashSet<ApplicationRole>();
        }

        public int ApplicationPermissionId { get; set; }

        public Guid Guid { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public string GroupName { get; set; }

        public virtual ICollection<ApplicationRole> Roles { get; set; }
    }
}
