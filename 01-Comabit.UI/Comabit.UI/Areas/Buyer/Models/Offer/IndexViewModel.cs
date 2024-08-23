// <copyright file="IndexViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Buyer.Models.Offer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Comabit.UI.Models.Match;

    public class IndexViewModel
    {
        public Guid? SelectedInquiryId { get; set; }

        public ICollection<InquiryViewModel> Inquiries { get; set; }

        public Guid? SelectedProjectId { get; set; }

        public ICollection<ProjectViewModel> Projects { get; set; }

        public ICollection<OfferViewModel> Offers { get; set; }

        public IndexViewModel()
        {
            this.Offers = new HashSet<OfferViewModel>();
            this.Inquiries= new HashSet<InquiryViewModel>();
        }

        public IndexViewModel(ICollection<OfferViewModel> offers)
        {
            this.Offers = offers;
            this.Inquiries = offers.Select(o => o.Match.Inquiry).Distinct().ToList();
        }
    }
}