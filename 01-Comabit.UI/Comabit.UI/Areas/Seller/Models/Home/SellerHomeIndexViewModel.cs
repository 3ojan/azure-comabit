using Comabit.UI.Areas.Shared.Models;
using Comabit.UI.Models.Match;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Seller.Models.Home
{
    public class SellerHomeIndexViewModel
    {
        public int CountNewInquiries { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double NewInquiriesSinceLastWeekPercent { get; set; }

        public int CountOpenOffers { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double OpenOffersSinceLastWeekPercent { get; set; }

        public int CountCommissions { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")] 
        public double CommissionsSinceLastWeekPercent { get; set; }

        public int CountInquiriesPotential { get; set; }

        public string InquiriesPotentialPercent { get; set; }

        public string InquiriesTrendJson { get; set; }

        public string CommissionByMonthJson { get; set; }

        public ICollection<MatchViewModel> Matches { get; set; }
    }
}
