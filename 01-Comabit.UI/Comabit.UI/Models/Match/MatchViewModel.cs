// <copyright file="MatchDetailViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Match
{
    using Comabit.DL.Data.Match;
    using Comabit.UI.Models.Company;
    using Microsoft.AspNetCore.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MatchViewModel
    {
        public Guid Id { get; set; }

        public MatchState State { get; set; }

        public double Score { get; set; }

        public RevokeReason RevokeReason { get; set; }

        public string RevokeReasonText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<OfferViewModel> Offers { get; set; }

        public OfferViewModel CurrentOffer
        {
            get
            {
                if (Offers != null && Offers.Any())
                {
                    return Offers.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
                }

                return null;
            }
        }

        public Guid InquiryId { get; set; }

        public InquiryViewModel Inquiry { get; set; }

        public Guid SellerId { get; set; }

        public SellerViewModel Seller { get; set; }

        public HtmlString ScoreBadge
        {
            get
            {
                string cssClass = string.Empty;

                if (Score >= 70)
                {
                    cssClass = "badge-success-light";
                }
                else if (Score >= 30)
                {
                    cssClass = "badge-info-light";
                }
                else
                {
                    cssClass = "badge-warning-light";
                }

                string result = string.Format("<span title=\"Übereinstimmung zwischen Bedarf und Portfolio des Verkäufers\" class=\"badge {0}\">{1} %</span>", cssClass, Score);

                return new HtmlString(result);
            }
        }

        public bool Checked { get; set; }

        public MatchViewModel()
        {
            this.Offers = new HashSet<OfferViewModel>();
            this.Checked = true;
        }
    }
}