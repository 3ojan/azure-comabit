// <copyright file="MessageController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Buyer.Controllers
{
    using Comabit.BL.Company;
    using Comabit.BL.Company.Dto;
    using Comabit.BL.Porfolio;
    using Comabit.UI.Areas.Admin.Models;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area(Roles.Buyer)]
    public class MessageController : BaseController
    {
        private CompanyManager CompanyManager { get; set; }

        public MessageController(CompanyManager companyManager, ILogger<HomeController> logger) : base(logger)
        {
            CompanyManager = companyManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Chat()
        {
            List<CompanyItem> companyItems = (await CompanyManager.GetAllBuyer()).ToList();
            companyItems.AddRange(await CompanyManager.GetAllSeller());
            List<CompanyViewModel> companies = Mapper.Map<List<CompanyViewModel>>(companyItems);

            return View(companies);
        }
    }
}