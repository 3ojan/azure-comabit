// <copyright file="SalesController.cs" company="mission-one">
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
    public class SalesController : BaseController
    {
        public InquiryManager _inquiryManager;

        public MatchManager _matchManager;

        public FileManager _fileManager;

        public SalesController(FileManager fileManager, MatchManager matchManager, InquiryManager inquiryManager, ILogger<MatchController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
            this._matchManager = matchManager;
            this._fileManager = fileManager;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOffer(CreateOfferViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CreateOfferItem createOfferItem = this.Mapper.Map<CreateOfferItem>(viewModel);
                await this._matchManager.AddOffer(createOfferItem);

                return new JsonNetResult(new
                {
                    status = "ok",
                    html = await this.RenderViewAsync("_CreateOfferOk"),
                });
            }

            return new JsonNetResult(new
            {
                status = "invalid",
                html = await this.RenderViewAsync("_CreateOffer", viewModel),
            });
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            MatchViewModel viewModel = this.Mapper.Map<MatchViewModel>(await this._matchManager.GetSellerMatch(id));
            
            return this.View(viewModel);
        }

        public async Task<ActionResult> Index()
        {
            ICollection<MatchViewModel> Matches = this.Mapper.Map<ICollection<MatchViewModel>>(await this._matchManager.GetSellerMatches(User.GetCompanyId(), new List<MatchState>() { MatchState.accepted, MatchState.renew }));

            IndexViewModel viewModel = new()
            {
                Matches = Matches,
            };

            return this.View(viewModel);
        }

        public async Task<ActionResult> Revoke(Guid id)
        {
            DeleteViewModel viewModel = this.Mapper.Map<DeleteViewModel>(await this._matchManager.GetSellerMatch(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Delete", viewModel),
            });
        }

        [HttpPost]
        public async Task<ActionResult> Revoke(DeleteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await this._matchManager.Revoke(viewModel.Id, (int)viewModel.RevokeReason, viewModel.RevokeReasonText);

                return new JsonNetResult(new
                {
                    status = "ok",
                });
            }

            return new JsonNetResult(new
            {
                status = "invalid",
                html = await this.RenderViewAsync("_Delete", viewModel),
            });
        }
    }
}