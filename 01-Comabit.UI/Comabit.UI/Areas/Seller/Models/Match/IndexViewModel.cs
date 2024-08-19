// <copyright file="MatchListViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Seller.Models.Match
{
    using Comabit.UI.Models.Match;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        public ICollection<MatchViewModel> Matches { get; set; }

        public IndexViewModel()
        {
            this.Matches = new HashSet<MatchViewModel>();
        }
    }
}