// <copyright file="BaseAuthenticationController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Controllers
{
    using Comabit.DL;
    using Comabit.UI.AutoMapper;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Security.Claims;
    using Users.Infrastructure;

    [Authorize]
    public class BaseController : Controller
    {
        public IMapper Mapper
        {
            get { return ViewModelMapper.Mapper; }
        }

        public ILogger<object> logger { get; set; }

        public CurrentUser CurrentUser { get; set; }

        public BaseController(ILogger<object> logger)
        {
            this.logger = logger;
            CurrentUser = new CurrentUser(ClaimsPrincipal.Current);
        }
    }
}