// <copyright file="AreaViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Portfolio
{
    using System;
    using System.Collections.Generic;

    public class AreaViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool ShowAdditionalTags { get; set; }

        public ICollection<CategoryViewModel> PortfolioCategories { get; set; }

        public AreaViewModel() {
            this.PortfolioCategories = new HashSet<CategoryViewModel>();
        }
    }
}