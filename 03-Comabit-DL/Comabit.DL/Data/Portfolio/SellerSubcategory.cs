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
    public class SellerSubcategory
    {
        [Column(Order = 1)]
        public int SellerId { get; set; }

        [Column(Order = 2)]
        public int PortfolioSubcategoryId { get; set; }

        public Seller Seller { get; set; }

        public PortfolioSubCategory PortfolioSubcategory { get; set; }
    }
}