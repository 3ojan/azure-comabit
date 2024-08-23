// <copyright file="ClaimsRoles.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Users.Infrastructure
{
    using Comabit.DL;
    using Comabit.DL.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// http://tech.trailmax.info/2014/08/aspnet-identity-and-owin-who-is-who/
    /// </summary>
    public class ClaimsRoles
    {
        public static IEnumerable<Claim> CreateRoleClaims(ApplicationUser user, ApplicationRole role, IPermissionService permissionService)
        {
            List<Claim> claims = new List<Claim>();
            List<string> roleIds = new List<string>();

            claims.Add(new Claim(ComabitClaimTypes.FullName, user.FullName != null ? user.FullName : string.Empty));
            claims.Add(new Claim(ComabitClaimTypes.RoleId, role.Id));
            claims.Add(new Claim(ComabitClaimTypes.RoleName, role.Name));

            var Permissions = permissionService.GetPermissionForRole(role.Id);

            Permissions.ToList().ForEach(permission => claims.Add(new Claim(ComabitClaimTypes.Permission, permission.Value)));

            return claims;
        }

        public void RolesFor(ClaimsIdentity ident)
        {
            List<Claim> claims = new List<Claim>();
        }
    }
}