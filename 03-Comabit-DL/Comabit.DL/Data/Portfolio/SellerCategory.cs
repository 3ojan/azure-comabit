using Comabit.DL.Data.Company;
using Comabit.DL.Data.Inquiry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.Portfolio
{
    public class SellerCategory
    {
        [Column(Order = 1)]
        public int PortfolioCategoryId { get; set; }

        [Column(Order = 2)]
        public int SellerId { get; set; }

        public PortfolioCategory PortfolioCategory { get; set; }

        public Seller Seller { get; set; }
    }
}