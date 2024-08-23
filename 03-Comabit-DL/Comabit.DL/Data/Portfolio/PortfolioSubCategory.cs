// <copyright file="PortfolioAreaSubCategory.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Portfolio
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Inquiry;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PortfolioSubCategory
    {
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        public Guid PortfolioCategoryId
        {
            get;
            set;
        }

        public PortfolioCategory PortfolioCategory
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<Seller> SellerCompanies { get; set; }

        public ICollection<Inquiry> BuyerProjectInquire { get; set; }

        public PortfolioSubCategory()
        {
            this.SellerCompanies = new HashSet<Seller>();
            this.BuyerProjectInquire = new HashSet<Inquiry>();
        }
    }
}