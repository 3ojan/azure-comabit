// <copyright file="InquiryViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Match
{
    using Comabit.BL.Helpers.Enumerations;
    using Comabit.DL.Data.Inquiry;
    using Comabit.UI.Models.File;
    using Comabit.UI.Models.Portfolio;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InquiryViewModel
    {
        public Guid Id { get; set; }

        public int InquiryNumber { get; set; }

        public string Purepose { get; set; }

        public Guid ProjectId { get; set; }

        public ProjectViewModel Project { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime Deadline { get; set; }

        public string DeadlineAsString
        {
            get
            {
                return Deadline.ToString("dd.MM.yyyy");
            }
        }

        public string DeadlineStateBadgeClass
        {
            get
            {
                if (DeadlineState == InquiryDeadline.active)
                {
                    return "badge-success-light";
                }
                else if (DeadlineState == InquiryDeadline.invoke)
                {
                    return "badge-warning-light";
                }

                return "badge-danger-light";
            }
        }

        public InquiryDeadline DeadlineState
        {
            get
            {
                if (IsPublished)
                {
                    if (Deadline > DateTime.Now)
                    {
                        return InquiryDeadline.active;
                    }

                    return InquiryDeadline.over;
                }

                return InquiryDeadline.invoke;
            }
        }

        public string DeadlineStateDescription
        {
            get
            {
                return DeadlineState.GetDescription();
            }
        }

        public string DeadlineInfo { get; set; }

        public string DeliveryPostalCode { get; set; }

        public string DeliveryCity { get; set; }

        public string DeliveryStreet { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryInfo { get; set; }

        public string AddidtionalTags { get; set; }

        public string Notes { get; set; }

        public string Requirements { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedAt { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsClosed { get; set; }

        public PlacingState PlacingState { get; set; }

        public ICollection<CategoryViewModel> PortfolioCategories { get; set; }

        public ICollection<SubCategoryViewModel> PortfolioSubCategories { get; set; }

        public ICollection<FileViewModel> Files { get; set; }

        public ICollection<MatchViewModel> Matches { get; set; }

        public InquiryViewModel()
        {
            this.PortfolioCategories = new HashSet<CategoryViewModel>();
            this.PortfolioSubCategories = new HashSet<SubCategoryViewModel>();
            this.Files = new HashSet<FileViewModel>();
            this.Matches = new HashSet<MatchViewModel>();
        }

        public ICollection<KeyValuePair<string, string>> SelectedPortfolios
        {
            get
            {
                List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

                if (PortfolioCategories.Any())
                {
                    result.AddRange(PortfolioCategories.Select(c => new KeyValuePair<string, string>(c.PortfolioArea.Name, c.Name)).ToList());
                }

                return result;
            }
        }

        public string FirstPortfolioAsString
        {
            get
            {
                string result = string.Empty;

                if (PortfolioCategories.Any())
                {
                    result = PortfolioCategories.FirstOrDefault().PortfolioArea.Name + " - " + PortfolioCategories.FirstOrDefault().Name;
                }

                return result;
            }
        }
    }
}