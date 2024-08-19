// <copyright file="BaseAuthenticationController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Authentication.Controllers
{
    using Comabit.DL;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    [Authorize]
    public class BaseAuthenticationController : BaseController
    {
        public readonly SignInManager<ApplicationUser> SignInManager;

        public readonly RoleManager<ApplicationRole> RoleManager;

        public BaseAuthenticationController(SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ILogger<object> logger): base(logger)
        {
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
    }
}