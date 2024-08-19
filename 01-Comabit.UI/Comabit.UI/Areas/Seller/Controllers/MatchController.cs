// <copyright file="MatchController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Seller.Controllers
{
    using Comabit.BL.File;
    using Comabit.BL.Inquiry;
    using Comabit.BL.Match;
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
    public class MatchController : BaseController
    {
        public InquiryManager _inquiryManager;

        public MatchManager _matchManager;

        public FileManager _fileManager;

        public MatchController(FileManager fileManager, MatchManager matchManager, InquiryManager inquiryManager, ILogger<MatchController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
            this._matchManager = matchManager;
            this._fileManager = fileManager;
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

                MatchViewModel detailViewModel = this.Mapper.Map<MatchViewModel>(await this._matchManager.GetSellerMatch(viewModel.Id));

                return new JsonNetResult(new
                {
                    status = "ok",
                    html = await this.RenderViewAsync("_Match", detailViewModel),
                });
            }

            return new JsonNetResult(new
            {
                status = "invalid",
                html = await this.RenderViewAsync("_Delete", viewModel),
            });
        }

        public async Task<ActionResult> Restore(Guid id)
        {
            MatchViewModel viewModel = this.Mapper.Map<MatchViewModel>(await this._matchManager.GetSellerMatch(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Restore", viewModel),
            });
        }

        [HttpPost]
        public async Task<ActionResult> Restore(MatchViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await this._matchManager.Restore(viewModel.Id);

                viewModel = this.Mapper.Map<MatchViewModel>(await this._matchManager.GetSellerMatch(viewModel.Id));

                return new JsonNetResult(new
                {
                    status = "ok",
                    html = await this.RenderViewAsync("_Match", viewModel),
                });
            }

            return new JsonNetResult(new
            {
                status = "invalid",
                html = await this.RenderViewAsync("_Restore", viewModel),
            });
        }

        public async Task<ActionResult> Index()
        {
            ICollection<MatchViewModel> Matches = this.Mapper.Map<ICollection<MatchViewModel>>(await this._matchManager.GetSellerMatches(User.GetCompanyId(), new List<MatchState>() { MatchState.pending, MatchState.revoked }));

            IndexViewModel viewModel = new()
            {
                Matches = Matches,
            };

            return this.View(viewModel);
        }

        public async Task<ActionResult> AddToSales(Guid id)
        {
            MatchViewModel viewModel = this.Mapper.Map<MatchViewModel>(await this._matchManager.GetSellerMatch(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_AddToSales", viewModel),
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddToSales(MatchViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await this._matchManager.AddToSales(viewModel.Id);

                return new JsonNetResult(new
                {
                    status = "ok",
                });
            }

            return new JsonNetResult(new
            {
                status = "invalid",
                html = await this.RenderViewAsync("Delete", viewModel),
            });
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