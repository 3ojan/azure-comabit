// <copyright file="CategoryViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Portfolio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryViewModel
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool Checked { get; set; }

        public string AdditionalPortfolioCategoryTagsAsString { get; set; }

        public AreaViewModel PortfolioArea
        {
            get;
            set;
        }

        public IEnumerable<SubCategoryViewModel> PortfolioSubCategories
        {
            get;
            set;
        }

        public CategoryViewModel()
        {
            this.PortfolioSubCategories = new HashSet<SubCategoryViewModel>();
        }
    }
}