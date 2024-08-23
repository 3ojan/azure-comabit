// <copyright file="SellerCompany.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Company
{
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Data.Portfolio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Comabit.DL.Data.Inquiry;

    public class Seller : Company
    {
        public ICollection<PortfolioCategory> PortfolioCategories { get; set; }

        public ICollection<PortfolioSubCategory> PortfolioSubCategories { get; set; }
        
        public ICollection<AdditionalPortfolioCategoryTags> AdditionalPortfolioCategoryTags { get; set; }

        public ICollection<Community> Communities { get; set; }

        public ICollection<DL.Data.Match.Match> Matches { get; set; }

        public ICollection<InquirySellerExclusion> InquiryExclusions { get; set; }

        //public ICollection<SellerCategory> Categories { get; set; }

        //public ICollection<SellerSubcategory> Subcategories { get; set; }

        public Seller()
        {
            this.PortfolioCategories = new HashSet<PortfolioCategory>();
            this.PortfolioSubCategories = new HashSet<PortfolioSubCategory>();
            this.AdditionalPortfolioCategoryTags = new HashSet<AdditionalPortfolioCategoryTags>();
            this.Communities = new HashSet<Community>();
            this.Matches = new HashSet<Match.Match>();
            this.InquiryExclusions = new HashSet<InquirySellerExclusion>();
            //this.Categories = new HashSet<SellerCategory>();
            //this.Subcategories = new HashSet<SellerSubcategory>();
        }
    }
}