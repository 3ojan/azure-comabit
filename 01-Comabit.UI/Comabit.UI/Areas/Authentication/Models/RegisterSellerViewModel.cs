// <copyright file="RegisterViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using Comabit.UI.Models.Portfolio;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [BindProperties]
    public class RegisterSellerViewModel : RegisterBaseViewModel
    {
        public ICollection<AreaViewModel> PortfolioAreas { get; set; }

        public ICollection<StateViewModel> States { get; set; }

        public string SelectedState { get; set; }

        public ICollection<CommunityViewModel> Communities { get; set; }

        public Guid NewCommunityId { get; set; }
    }
}