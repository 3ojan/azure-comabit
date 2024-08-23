// <copyright file="EditViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Buyer.Models.Inquiry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Comabit.BL.Helpers.Attributes;
    using Comabit.DL.Data.Inquiry;
    using Comabit.Helpers;
    using Comabit.UI.Models.File;
    using Comabit.UI.Models.Match;
    using Comabit.UI.Models.Portfolio;
    using Microsoft.AspNetCore.Http;

    public class EditViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string Purepose { get; set; }
        
        public Guid ProjectId { get; set; }

        public ProjectViewModel Project { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Die Bieterfrist muss in der Zukunft liegen.")]
        public DateTime? Deadline { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string DeadlineInfo { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string DeliveryStreet { get; set; }

        [Required]
        [RegularExpression(Validation.IsPostalcodeDeOrAtValidRegEx)]
        public string DeliveryPostalCode { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string DeliveryCity { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Der Liefertermin muss in der Zukunft liegen.")]
        public DateTime? DeliveryDate { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string DeliveryInfo { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string AddidtionalTags { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string Notes { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string Requirements { get; set; }

        public bool IsPublished { get; set; }

        [RequiredIf("IsPublishedAtRequired")]
        [FutureDate(ErrorMessage = "Veröffentlichen am muss in der Zukunft liegen.")]
        public DateTime? PublishedAt { get; set; }

        public bool IsPublishedAtRequired
        {
            get
            {
                return PublishState == PublishState.publishAt;
            }
        }

        public bool IsCanceled { get; set; }

        public bool IsClosed { get; set; }

        public PlacingState PlacingState { get; set; }

        public ICollection<Guid> ExcludedSellerIds { get; set; }

        public ICollection<MatchViewModel> MatchesPreview { get; set; }

        public ICollection<AreaViewModel> PortfolioAreas { get; set; }

        public ICollection<FileViewModel> Files { get; set; }

        public IEnumerable<IFormFile> UploadedFiles { get; set; }

        public PublishState PublishState { get; set; }

        public EditViewModel()
        {
            this.PortfolioAreas = new HashSet<AreaViewModel>();
            this.Files = new HashSet<FileViewModel>();
            this.UploadedFiles = new HashSet<IFormFile>();
            this.MatchesPreview = new HashSet<MatchViewModel>();
            this.ExcludedSellerIds = new HashSet<Guid>();
        }

        public EditViewModel(ProjectViewModel project, ICollection<AreaViewModel> areas)
        {
            this.PortfolioAreas = new HashSet<AreaViewModel>();
            this.Files = new HashSet<FileViewModel>();
            this.UploadedFiles = new HashSet<IFormFile>();
            this.MatchesPreview = new HashSet<MatchViewModel>();
            this.ExcludedSellerIds = new HashSet<Guid>();

            Project = project;
            ProjectId = project.Id;
            PortfolioAreas = areas;

            DeliveryCity = Project.City;
            DeliveryPostalCode = Project.PostalCode;
            DeliveryStreet = Project.Street;
        }
    }
}