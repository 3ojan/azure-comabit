using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Porfolio.Dto
{
    public class AdditionalPortfolioCategoryTagsItem
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid SellerCompanyId
        {
            get;
            set;
        }

        public Guid PortfolioCategoryId
        {
            get;
            set;
        }

        public string Tags
        {
            get;
            set;
        }
    }
}