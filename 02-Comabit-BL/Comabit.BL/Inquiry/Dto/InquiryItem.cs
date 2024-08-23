using Comabit.BL.Match.Dto;
using Comabit.BL.Porfolio.Dto;
using Comabit.DL.Data.Inquiry;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Inquiry.Dto
{
    public class InquiryItem
    {
        public Guid Id { get; set; }

        public int InquiryNumber { get; set; }

        public string Purepose { get; set; }

        public Guid ProjectId { get; set; }

        public ProjectItem Project { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedByUserId { get; set; }

        public DateTime Deadline { get; set; }

        public string DeadlineInfo { get; set; }

        public string DeliveryPostalCode { get; set; }

        public string DeliveryCity { get; set; }

        public string DeliveryStreet { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryInfo { get; set; }

        public string AddidtionalTags { get; set; }

        public string Notes { get; set; }

        public string Requirements { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedAt { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsClosed { get; set; }

        public PlacingState PlacingState { get; set; }

        public ICollection<PortfolioAreaItem> PortfolioAreas { get; set; }

        public ICollection<PortfolioCategoryItem> PortfolioCategories { get; set; }

        public ICollection<PortfolioSubCategoryItem> PortfolioSubCategories { get; set; }

        public ICollection<FileItem> Files { get; set; }

        public IEnumerable<IFormFile> UploadedFiles { get; set; }

        public ICollection<MatchItem> Matches { get; set; }

        public PublishState PublishState { get; set; }

        public ICollection<InquirySellerExclusionItem> ExcludedSellers { get; set; }

        public ICollection<Guid> ExcludedSellerIds { get; set; }

        public InquiryItem()
        {
            this.Matches = new HashSet<MatchItem>();
            this.PortfolioSubCategories = new HashSet<PortfolioSubCategoryItem>();
            this.PortfolioCategories = new HashSet<PortfolioCategoryItem>();
            this.PortfolioAreas = new HashSet<PortfolioAreaItem>();
            this.Files = new HashSet<FileItem>();
            this.ExcludedSellerIds = new HashSet<Guid>();
            this.ExcludedSellers = new HashSet<InquirySellerExclusionItem>();
        }
    }
}