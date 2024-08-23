using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comabit.DL.Data.Portfolio
{
    public class PortfolioArea
    {
        [Key]
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

        public ICollection<PortfolioCategory> PortfolioCategories
        {
            get;
            set;
        }
    }
}