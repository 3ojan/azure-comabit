using Comabit.DL.Data.Company;
using Comabit.DL.Data.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.Portfolio
{
    public class AdditionalPortfolioCategoryTags
    {
        public Guid Id { get; set; }

        public Guid SellerCompanyId { get; set; }

        public Seller SellerCompany { get; set; }

        public Guid PortfolioCategoryId { get; set; }

        public PortfolioCategory PortfolioCategory { get; set; }

        public string Tags { get; set; }
    }
}