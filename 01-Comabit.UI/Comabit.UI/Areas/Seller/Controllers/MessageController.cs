// <copyright file="MessageController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Seller.Controllers
{
    using Comabit.BL.Porfolio;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Users.Infrastructure;

    [Area(Roles.Seller)]
    public class MessageController : BaseController
    {
        private PortfolioManager _portfolioManager;

        public MessageController(PortfolioManager portfolioManager, ILogger<HomeController> logger) : base(logger)
        {
            this._portfolioManager = portfolioManager;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}