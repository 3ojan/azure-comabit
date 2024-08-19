using Comabit.BL.Company;
using Comabit.BL.Identity;
using Comabit.BL.Inquiry;
using Comabit.BL.Match;
using Comabit.BL.Porfolio;
using Comabit.DL.Data.Match;
using Comabit.Helpers;
using Comabit.UI.Areas.Buyer.Models.Offer;
using Comabit.UI.Controllers;
using Comabit.UI.Models.Match;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Infrastructure;

namespace Comabit.UI.Areas.Buyer.Controllers
{
    [Area("Buyer")]
    public class OfferController : BaseController
    {
        private readonly MatchManager _matchManager;

        private readonly InquiryManager _inquiryManager;

        public OfferController(MatchManager matchManager, InquiryManager inquiryManager, ILogger<OfferController> logger) : base(logger)
        {
            this._matchManager = matchManager;
            this._inquiryManager = inquiryManager;
        }

        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            if (vm.SelectedInquiryId.HasValue)
            {
                vm.Offers = this.Mapper.Map<ICollection<OfferViewModel>>(await this._matchManager.GetOffersForInquiry(vm.SelectedInquiryId.Value));

                if (vm.Offers.Any())
                {
                    vm.SelectedProjectId = vm.Offers.FirstOrDefault().Match.Inquiry.ProjectId;
                    vm.Inquiries = this.Mapper.Map<ICollection<InquiryViewModel>>((await this._inquiryManager.GeInquiriesNames(User.GetCompanyId())).Where(i => i.Matches.Any(m => m.Offers.Any() && m.Inquiry.ProjectId == vm.SelectedProjectId.Value)));
                }
            }
            else if (vm.SelectedProjectId.HasValue)
            {
                vm.Offers = this.Mapper.Map<ICollection<OfferViewModel>>(await this._matchManager.GetOffersForProject(vm.SelectedProjectId.Value));
                vm.Inquiries = this.Mapper.Map<ICollection<InquiryViewModel>>((await this._inquiryManager.GeInquiriesNames(User.GetCompanyId())).Where(i => i.Matches.Any(m => m.Offers.Any() && m.Inquiry.ProjectId == vm.SelectedProjectId.Value)));
            }
            else
            {
                vm.Offers = this.Mapper.Map<ICollection<OfferViewModel>>(await this._matchManager.GetOffersForBuyer(User.GetCompanyId()));
                vm.Inquiries = this.Mapper.Map<ICollection<InquiryViewModel>>((await this._inquiryManager.GeInquiriesNames(User.GetCompanyId())).Where(i => i.Matches.Any(m => m.Offers.Any())));
            }
            
            vm.Projects = this.Mapper.Map<ICollection<ProjectViewModel>>((await this._inquiryManager.GetProjectNames(User.GetCompanyId())).Where(i => i.Inquiries.Any(i => i.Matches.Any(m => m.Offers.Any()))));
            
            return View(vm);
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            OfferViewModel offer = this.Mapper.Map<OfferViewModel>(await this._matchManager.GetOffer(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Detail", offer),
            });
        }

        public async Task<ActionResult> Edit(Guid id, OfferState state)
        {
            OfferViewModel offer = this.Mapper.Map<OfferViewModel>(await this._matchManager.GetOffer(id));
            offer.State = state;

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Edit", offer),
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OfferViewModel viewModel)
        {
            await this._matchManager.UpdateOffer(viewModel.Id, viewModel.State);

            OfferViewModel offer = this.Mapper.Map<OfferViewModel>(await this._matchManager.GetOffer(viewModel.Id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Offer", offer),
            });
        }

        [HttpPost]
        public async Task<ActionResult> SaveNote(OfferViewModel viewModel)
        {
            await this._matchManager.SaveOfferNote(viewModel.Id, viewModel.BuyerNote);

            OfferViewModel offer = this.Mapper.Map<OfferViewModel>(await this._matchManager.GetOffer(viewModel.Id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Offer", offer),
            });
        }

        public async Task<ActionResult> NewMessage(Guid id)
        {
            OfferViewModel offer = this.Mapper.Map<OfferViewModel>(await this._matchManager.GetOffer(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_Message", offer),
            });
        }
    }
}