// <copyright file="EditCommunitiesViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Seller.Models.Settings
{
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using System;
    using System.Collections.Generic;

    public class CommunitiesEditViewModel
    {
        public Guid CompanyId { get; set; }

        public ICollection<StateViewModel> States { get; set; }

        public string SelectedState { get; set; }

        public ICollection<CommunityViewModel> Communities { get; set; }

        public Guid NewCommunityId { get; set; }
    }
}