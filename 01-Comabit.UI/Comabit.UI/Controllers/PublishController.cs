// <copyright file="HomeController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.Controllers
{
    using Comabit.BL.Inquiry;
    using Comabit.BL.Inquiry.Dto;
    using Comabit.Helpers;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PublishController : BaseController
    {
        private readonly InquiryManager _inquiryManager;

        public PublishController(InquiryManager inquiryManager, ILogger<PublishController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var inquiries = await this._inquiryManager.GetInquiriesToPublish();

            int publishedInquiries = await this._inquiryManager.PublishInquiries(inquiries);
                        
            return new JsonNetResult(new
            {
                status = "ok",
                publishedInquiries,
            });
        }
    }
}