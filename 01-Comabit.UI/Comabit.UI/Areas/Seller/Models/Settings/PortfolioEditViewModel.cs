// <copyright file="PortfolioEditViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Seller.Models.Settings
{
    using Comabit.UI.Models.Portfolio;
    using System;
    using System.Collections.Generic;

    public class PortfolioEditViewModel
    {
        public ICollection<AreaViewModel> PortfolioAreas { get; set; }

        public Guid CompanyId { get; set; }

        public PortfolioEditViewModel(Guid companyId, ICollection<AreaViewModel> portfolioAreas)
        {
            PortfolioAreas = portfolioAreas;
            CompanyId = companyId;
        }

        public PortfolioEditViewModel()
        {

        }
    }
}