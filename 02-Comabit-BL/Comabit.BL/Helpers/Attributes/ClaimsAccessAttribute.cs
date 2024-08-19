// <copyright file="ClaimsAccessAttribute.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Users.Infrastructure
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;
    using System.Security.Claims;

    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(string permission) : base(typeof(HasPermissionFilter))
        {
            Arguments = new object[] { new Claim(ComabitClaimTypes.Permission, permission) };
        }
    }

    public class HasPermissionFilter: IAuthorizationFilter
    {
        readonly Claim _claim;

        public HasPermissionFilter(Claim claim) : base()
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}