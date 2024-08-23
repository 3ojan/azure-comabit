// <copyright file="UserExtended.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Users.Infrastructure
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;

    public static class UserExtended
    {
        public static string GetFullName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ComabitClaimTypes.FullName);

            return claim == null ? null : claim.Value;
        }

        public static string GetEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Email);
            return claim == null ? null : claim.Value;
        }

        public static bool HasPermission(this IPrincipal user, string permission)
        {
            return ((ClaimsIdentity)user.Identity).HasClaim(ComabitClaimTypes.Permission, permission);
        }

        public static bool HasCompany(this IPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).Claims.Where(c => c.Type == ComabitClaimTypes.CompanyGuid).Any();
        }

        public static Guid GetCompanyId(this IPrincipal user)
        {
            return new Guid(((ClaimsIdentity)user.Identity).Claims.Where(c => c.Type == ComabitClaimTypes.CompanyGuid).FirstOrDefault().Value);
        }

        public static string GetCompanyName(this IPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).Claims.Where(c => c.Type == ComabitClaimTypes.CompanyName).FirstOrDefault()?.Value;
        }

        public static bool IsPasswordExpired(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ComabitClaimTypes.PasswordExpired);

            bool isPasswordExpired = false;
            if(claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                bool.TryParse(claim.Value, out isPasswordExpired);
            }

            return isPasswordExpired;
        }

        public static bool HasRememberMe(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ComabitClaimTypes.RememberMe);

            bool rememberMe = false;
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                bool.TryParse(claim.Value, out rememberMe);
            }

            return rememberMe;
        }

        public static string GetRoleId(this IPrincipal user)
        {
            var result = string.Empty;
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ComabitClaimTypes.RoleId);
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                result = claim.Value;
            }

            return result;
        }

        public static string GetRoleName(this IPrincipal user)
        {
            var result = string.Empty;
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ComabitClaimTypes.RoleName);
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                result = claim.Value;
            }

            return result;
        }

        public static string GetUserId(this IPrincipal user)
        {

            var result = string.Empty;
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                result = claim.Value;
            }

            return result;
        }
    }
}