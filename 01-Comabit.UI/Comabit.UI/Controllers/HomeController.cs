// <copyright file="HomeController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.Controllers
{
    using Comabit.DL;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    public class HomeController : BaseController
    {
        public UserManager<ApplicationUser> userManager { get; set; }

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager) : base(logger)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                string roleName = User.GetRoleName();

                if (!string.IsNullOrEmpty(roleName) && roleName != Roles.Besucher)
                {
                    if (roleName == Roles.SuperAdmin) roleName = Roles.Admin;
                    
                    return RedirectToAction("Index", "Home", new { Area = roleName });
                }
            }

            return View(new HomeViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Exception = feature?.Error });
        }
    }
}