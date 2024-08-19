// <copyright file="ProjectController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Buyer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Comabit.BL.Inquiry;
    using Comabit.BL.Inquiry.Dto;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Buyer.Models.Project;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Match;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Users.Infrastructure;

    [Area("Buyer")]
    public class ProjectController : BaseController
    {
        private readonly InquiryManager _inquiryManager;

        public ProjectController(InquiryManager inquiryManager, ILogger<ProjectController> logger) : base(logger)
        {
            this._inquiryManager = inquiryManager;
        }

        public async Task<IActionResult> Index()
        {
            var buyerProjectItems = await this._inquiryManager.GetBuyerProjects(User.GetCompanyId());

            ICollection<ProjectViewModel> buyerProjects = this.Mapper.Map<ICollection<ProjectViewModel>>(buyerProjectItems);

            return View(buyerProjects);
        }

        public async Task<IActionResult> Create()
        {
            ICollection<ProjectViewModel> projects = this.Mapper.Map<ICollection<ProjectViewModel>>(await this._inquiryManager.GetBuyerProjects(User.GetCompanyId(), onlyActive: true));

            return View(new CreateEditViewModel() { AvailableProjects = projects });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Id = Guid.NewGuid();
                await this._inquiryManager.CreateProject(this.Mapper.Map<ProjectItem>(vm), User.GetUserId(), User.GetCompanyId());

                return RedirectToAction("Index");
            }
            else
            {
                vm.AvailableProjects = this.Mapper.Map<ICollection<ProjectViewModel>>(await this._inquiryManager.GetBuyerProjects(User.GetCompanyId(), onlyActive: true));

                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAndCreateInquiry(CreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Id = Guid.NewGuid();
                await this._inquiryManager.CreateProject(this.Mapper.Map<ProjectItem>(vm), User.GetUserId(), User.GetCompanyId());

                return RedirectToAction("Create", "Inquiry", new { Area = Roles.Buyer, projectId = vm.Id });
            }
            else
            {
                vm.AvailableProjects = this.Mapper.Map<ICollection<ProjectViewModel>>(await this._inquiryManager.GetBuyerProjects(User.GetCompanyId(), onlyActive: true));

                return View("Create", vm);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            CreateEditViewModel viewModel = this.Mapper.Map<CreateEditViewModel>(await this._inquiryManager.GetBuyerProjectById(id));

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var projectItem = this.Mapper.Map<CreateEditViewModel, ProjectItem>(vm);

                await this._inquiryManager.UpdateBuyerProject(projectItem);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetProjectDetails(CreateEditViewModel viewModel)
        {
            if (viewModel.SelectedProject.HasValue)
            {
                ProjectViewModel project = this.Mapper.Map<ProjectViewModel>(await this._inquiryManager.GetBuyerProjectById(viewModel.SelectedProject.Value));

                return new JsonNetResult(new
                {
                    status = "ok",
                    html = await this.RenderViewAsync("_ProjectDetail", project),
                });
            }

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_ProjectForm", viewModel),
            });
        }
    }
}