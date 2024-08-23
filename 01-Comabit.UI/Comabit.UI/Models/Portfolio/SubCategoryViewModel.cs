// <copyright file="SubCategoryViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models.Portfolio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubCategoryViewModel
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

        public string AdditionalPortfolioCategoryTags { get; set; }

        public CategoryViewModel PortfolioCategory { get; set; }
    }
}