// <copyright file="OrderController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Seller.Controllers
{
    using Comabit.BL.File;
    using Comabit.BL.Inquiry;
    using Comabit.BL.Match;
    using Comabit.BL.Match.Dto;
    using Comabit.DL.Data.Match;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Seller.Models.Match;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [Area(Roles.Seller)]
    public class OrderController : BaseController
    {
        public InquiryManager _inquiryManager;

        public MatchManager _matchManager;

        public FileManager _fileManager;

        public OrderController(FileManager fileManager, MatchManager matchManager, InquiryManager inquiryManager, ILogger<OrderController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
            this._matchManager = matchManager;
            this._fileManager = fileManager;
        } 

        public async Task<ActionResult> Index()
        {
            ICollection<MatchViewModel> Matches = this.Mapper.Map<ICollection<MatchViewModel>>(await this._matchManager.GetSellerMatches(User.GetCompanyId(), new List<MatchState>() { MatchState.ordered }));

            IndexViewModel viewModel = new()
            {
                Matches = Matches,
            };

            return this.View(viewModel);
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            MatchViewModel viewModel = this.Mapper.Map<MatchViewModel>(await this._matchManager.GetSellerMatch(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Detail", viewModel),
            });
        }
    }
}