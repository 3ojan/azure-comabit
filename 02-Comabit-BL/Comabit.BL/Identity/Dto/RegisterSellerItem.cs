// <copyright file="RegisterSellerItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Identity.Dto
{
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Porfolio.Dto;
    using System;
    using System.Collections.Generic;

    public class RegisterSellerItem: RegisterBaseItem
    {
        public ICollection<PortfolioAreaItem> PortfolioAreas { get; set; }

        public ICollection<StateItem> States { get; set; }

        public Guid SelectedState { get; set; }

        public ICollection<CommunityItem> Communities { get; set; }
    }
}