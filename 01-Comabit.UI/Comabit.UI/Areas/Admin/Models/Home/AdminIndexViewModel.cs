// <copyright file="IndexViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Admin.Models.Home
{
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using Comabit.UI.Models.Match;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AdminIndexViewModel
    {
        public int CountBuyers { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double NewBuyersSinceLastWeekPercent { get; set; }

        public int CountSellers { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double NewSellersSinceLastWeekPercent { get; set; }

        public int CountUserOnline { get; set; }

        public string BuyerSellerTrendJson { get; set; }

        public ICollection<LogViewModel> Logs { get; set; }

        public AdminIndexViewModel()
        {
            Logs = new HashSet<LogViewModel>();
        }
    }
}