using Comabit.BL.Match.Dto;
using Comabit.UI.Models.Match;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Buyer.Models.Home
{
    public class BuyerHomeIndexViewModel
    {
        public int CountNewOffers { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double NewOffersSinceLastWeekPercent { get; set; }

        public int CountOrderedMatches { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double OrderedMatchesSinceLastWeekPercent { get; set; }

        public string OffersTrendJson { get; set; }

        public ICollection<InquiryViewModel> InquiriesOrderByCreated { get; set; }

        public ICollection<InquiryViewModel> InquiriesOrderByUpdated { get; set; }

        public ICollection<OfferViewModel> Offers { get; set; }
    }
}
