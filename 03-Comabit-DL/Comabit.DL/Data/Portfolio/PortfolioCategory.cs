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
    public class PortfolioCategory
    {
        [Key]
        public Guid Id
        {
            get;
            set;
        }

        public Guid PortfolioAreaId
        {
            get;
            set;
        }

        public PortfolioArea PortfolioArea
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<PortfolioSubCategory> PortfolioSubCategories
        {
            get;
            set;
        }

        public ICollection<AdditionalPortfolioCategoryTags> AdditionalPortfolioCategoryTags { get; set; }

        public ICollection<Seller> SellerCompanies { get; set; }

        public ICollection<Inquiry.Inquiry> BuyerProjectInquire { get; set; }

        //public ICollection<SellerCategory> Sellers { get; set; }

        public PortfolioCategory()
        {
            this.PortfolioSubCategories = new HashSet<PortfolioSubCategory>();
            this.AdditionalPortfolioCategoryTags = new HashSet<AdditionalPortfolioCategoryTags>();
            this.SellerCompanies = new HashSet<Seller>();
            this.BuyerProjectInquire = new HashSet<Inquiry.Inquiry>();
        }
    }
}