// <copyright file="PortfolioSubCategory.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Porfolio.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PortfolioSubCategoryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }

        public Guid PortfolioAreaCategoryId { get; set; }

        public PortfolioCategoryItem PortfolioCategory { get; set; }
    }
}