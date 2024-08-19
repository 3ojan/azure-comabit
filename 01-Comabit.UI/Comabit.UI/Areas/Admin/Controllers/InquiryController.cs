// <copyright file="InquiryController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Admin.Controllers
{
    using Comabit.BL.Company;
    using Comabit.BL.Company.Dto;
    using Comabit.BL.Inquiry;
    using Comabit.DL;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Admin.Models;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area("Admin")]
    public class InquiryController : BaseController
    {
        private readonly InquiryManager _inquiryManager;

        public InquiryController(InquiryManager inquiryManager, ILogger<InquiryController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
        }

        public async Task<ActionResult> Get(Guid id)
        {
            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(id));

            return new JsonNetResult(new
            {
                status = "ok",
                Inquiry = inquiry,
                test = await this._inquiryManager.GetInquiry(id)
            });
        }
    }
}