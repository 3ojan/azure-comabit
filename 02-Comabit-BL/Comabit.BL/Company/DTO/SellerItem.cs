using Comabit.BL.Geo.Dto;
using Comabit.BL.Porfolio.Dto;
using Comabit.DL.Data.Portfolio;
using Comabit.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Company.Dto
{
    public class SellerItem : CompanyItem
    {
        public ICollection<PortfolioCategoryItem> PortfolioCategories { get; set; }

        public ICollection<PortfolioSubCategoryItem> PortfolioSubCategories { get; set; }

        public ICollection<AdditionalPortfolioCategoryTagsItem> AdditionalPortfolioCategoryTags { get; set; }

        public ICollection<CommunityItem> Communities { get; set; }
    }
}