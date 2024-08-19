using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Porfolio.Dto
{
    public class PortfolioAreaItem
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool ShowAdditionalTags { get; set; }

        public ICollection<PortfolioCategoryItem> PortfolioCategories
        {
            get;
            set;
        }

        public PortfolioAreaItem()
        {
            this.PortfolioCategories = new HashSet<PortfolioCategoryItem>();
        }
    }
}