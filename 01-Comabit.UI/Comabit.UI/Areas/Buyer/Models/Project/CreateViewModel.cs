// <copyright file="CreateViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Comabit.Helpers;
using Comabit.UI.Models.Match;

namespace Comabit.UI.Areas.Buyer.Models.Project
{
    public class CreateEditViewModel
    {
        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string ProjectName { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string Street { get; set; }

        [Required]
        [RegularExpression(Validation.IsPostalcodeDeOrAtValidRegEx)]
        public string PostalCode { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaValidRegEx)]
        public string City { get; set; }

        [Required]
        [RegularExpression(Validation.IsEmailValidRegEx)]
        public string ContactEmail { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaValidRegEx)]
        public string ContactClerk { get; set; }

        public ICollection<ProjectViewModel> AvailableProjects { get; set; }

        public Guid? SelectedProject { get; set; }

        public Guid? Id { get; set; }

        public bool IsActive { get; set; }
    }
}