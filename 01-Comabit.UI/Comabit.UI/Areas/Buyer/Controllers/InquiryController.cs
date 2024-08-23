// <copyright file="InquiryController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Buyer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Comabit.BL.ElasticSearch;
    using Comabit.BL.Inquiry;
    using Comabit.BL.Inquiry.Dto;
    using Comabit.BL.Match;
    using Comabit.BL.Match.Dto;
    using Comabit.BL.Porfolio;
    using Comabit.BL.Porfolio.Dto;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Portfolio;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Buyer.Models.Inquiry;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Comabit.UI.Models.Portfolio;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Users.Infrastructure;

    [Area("Buyer")]
    public class InquiryController : BaseController
    {
        private readonly InquiryManager _inquiryManager;
        private readonly PortfolioManager _portfolioManager;
        private readonly MatchManager _matchManager;
        private readonly ElasticSearchManager _elasticSearchManager;

        public InquiryController(ElasticSearchManager elasticSearchManager, MatchManager matchManager, InquiryManager inquiryManager, PortfolioManager portfolioManager, ILogger<InquiryController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
            this._portfolioManager = portfolioManager;
            this._matchManager = matchManager;
            this._elasticSearchManager = elasticSearchManager;
        }

        public async Task<IActionResult> Index(IndexViewModel viewModel)
        {
            ICollection<InquiryViewModel> inquiries;

            if (viewModel.SelectedProjectId.HasValue)
            {
                inquiries = this.Mapper.Map<ICollection<InquiryViewModel>>(await this._inquiryManager.GetInquiriesForProject(viewModel.SelectedProjectId.Value));
            }
            else
            {
                inquiries = this.Mapper.Map<ICollection<InquiryViewModel>>(await this._inquiryManager.GetInquiries(User.GetCompanyId()));
            }

            ICollection<ProjectViewModel> projects = this.Mapper.Map<ICollection<ProjectViewModel>>(await this._inquiryManager.GetBuyerProjects(User.GetCompanyId(), onlyActive: false));

            return View(new IndexViewModel(inquiries, projects) { SelectedProjectId = viewModel.SelectedProjectId });
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            InquiryItem inquiryItem = await this._inquiryManager.GetInquiryForEdit(id);
            EditViewModel viewModel = this.Mapper.Map<EditViewModel>(inquiryItem);

            ICollection<MatchItem> matchItems = await this._elasticSearchManager.SearchMatchesForInquiry(inquiryItem);
            viewModel.MatchesPreview = this.Mapper.Map<ICollection<MatchViewModel>>(matchItems);

            foreach (var match in viewModel.MatchesPreview)
            {
                match.Checked = !viewModel.ExcludedSellerIds.Contains(match.SellerId);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            InquiryItem inquiryItem = this.Mapper.Map<InquiryItem>(viewModel);

            if (ModelState.IsValid)
            {
                await this._inquiryManager.UpdateInquiry(inquiryItem, User.GetUserId());
                 
                return RedirectToAction("Index", "Inquiry", new { Area = Roles.Buyer, SelectedProjectId = viewModel.ProjectId });
            }

            viewModel.Project = this.Mapper.Map<ProjectViewModel>(await this._inquiryManager.GetBuyerProjectById(viewModel.ProjectId));

            var categories = viewModel.PortfolioAreas.SelectMany(p => p.PortfolioCategories?.Where(c => c.Checked));
            var subcategories = categories.SelectMany(c => c.PortfolioSubCategories).Where(s => s.Checked).ToList();

            inquiryItem.PortfolioCategories = this.Mapper.Map<ICollection<PortfolioCategoryItem>>(categories);
            inquiryItem.PortfolioSubCategories = this.Mapper.Map<ICollection<PortfolioSubCategoryItem>>(subcategories);

            ICollection<MatchItem> matchItems = await this._elasticSearchManager.SearchMatchesForInquiry(inquiryItem);
            viewModel.MatchesPreview = this.Mapper.Map<ICollection<MatchViewModel>>(matchItems);

            foreach (var match in viewModel.MatchesPreview)
            {
                match.Checked = !viewModel.ExcludedSellerIds.Contains(match.SellerId);
            }

            return this.View(viewModel);
        }

        public async Task<ActionResult> Create(Guid projectId)
        {
            ProjectViewModel project = this.Mapper.Map<ProjectViewModel>(await this._inquiryManager.GetBuyerProjectById(projectId));
            ICollection<AreaViewModel> areas = this.Mapper.Map<ICollection<AreaViewModel>>(await this._portfolioManager.RetrievePortfolioAreas());

            return this.View(new EditViewModel(project, areas));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await this._inquiryManager.CreateInquiry(this.Mapper.Map<InquiryItem>(viewModel), User.GetUserId());

                return RedirectToAction("Index", "Inquiry", new { Area = Roles.Buyer, SelectedProjectId = viewModel.ProjectId });
            }

            viewModel.Project = this.Mapper.Map<ProjectViewModel>(await this._inquiryManager.GetBuyerProjectById(viewModel.ProjectId));

            return this.View(viewModel);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            InquiryViewModel project = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiryForEdit(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Detail", project),
            });
        }

        public async Task<IActionResult> Matches(Guid inquiryId)
        {
            ICollection<MatchViewModel> matches = this.Mapper.Map<ICollection<MatchViewModel>>(await this._matchManager.GetInquiryMatches(inquiryId));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Matches", matches),
            });
        }

        public async Task<ActionResult> Publish(Guid id)
        {
            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Publish", inquiry),
            });
        }

        [HttpPost]
        public async Task<ActionResult> Publish(InquiryViewModel viewModel)
        {
            await this._inquiryManager.PublishInquiry(viewModel.Id, User.GetUserId());

            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(viewModel.Id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Inquiry", inquiry),
            });
        }

        public async Task<ActionResult> Cancel(Guid id)
        {
            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Cancel", inquiry),
            });
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(InquiryViewModel viewModel)
        {
            await this._inquiryManager.CancelInquiry(viewModel.Id, User.GetUserId());

            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(viewModel.Id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Inquiry", inquiry),
            });
        }

        public async Task<ActionResult> EditState(Guid id)
        {
            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_EditState", inquiry),
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditState(InquiryViewModel viewModel)
        {
            await this._inquiryManager.UpdateInquiryState(this.Mapper.Map<InquiryItem>(viewModel), User.GetUserId());

            InquiryViewModel inquiry = this.Mapper.Map<InquiryViewModel>(await this._inquiryManager.GetInquiry(viewModel.Id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Inquiry", inquiry),
            });
        }

        [HttpPost]
        public async Task<ActionResult> MatchesPreview(EditViewModel viewModel)
        {
            var inquiry = this.Mapper.Map<InquiryItem>(viewModel);
            var categories = viewModel.PortfolioAreas.SelectMany(p => p.PortfolioCategories?.Where(c => c.Checked));
            var subcategories = categories.SelectMany(c => c.PortfolioSubCategories).Where(s => s.Checked).ToList();
            
            inquiry.PortfolioCategories = this.Mapper.Map<ICollection<PortfolioCategoryItem>>(categories);
            inquiry.PortfolioSubCategories = this.Mapper.Map<ICollection<PortfolioSubCategoryItem>>(subcategories);

            ICollection<MatchItem> matchItems = await this._elasticSearchManager.SearchMatchesForInquiry(inquiry);
            ICollection<MatchViewModel> matches = this.Mapper.Map<ICollection<MatchViewModel>>(matchItems);

            foreach (var match in matches)
            {
                match.Checked = !viewModel.ExcludedSellerIds.Contains(match.SellerId);
            }

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_MatchesPreview", matches),
            });
        }
    }
}