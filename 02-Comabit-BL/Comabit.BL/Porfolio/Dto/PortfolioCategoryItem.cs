namespace Comabit.BL.Porfolio.Dto
{
    using Comabit.BL.Company.Dto;
    using Comabit.BL.Inquiry.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PortfolioCategoryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PortfolioAreaId { get; set; }

        public bool Checked { get; set; }

        public string AdditionalPortfolioCategoryTagsAsString { get; set; }

        public PortfolioAreaItem PortfolioArea { get; set; }

        public ICollection<PortfolioSubCategoryItem> PortfolioSubCategories { get; set; }

        public ICollection<SellerItem> SellerCompanies { get; set; }

        public ICollection<InquiryItem> BuyerProjectInquire { get; set; }

        public PortfolioCategoryItem()
        {
            this.PortfolioSubCategories = new HashSet<PortfolioSubCategoryItem>();
            this.SellerCompanies = new HashSet<SellerItem>();
            this.BuyerProjectInquire = new HashSet<InquiryItem>();
        }
    }
}