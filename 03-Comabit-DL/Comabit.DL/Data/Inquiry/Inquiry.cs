using Comabit.DL.Data.Company;
using Comabit.DL.Data.File;
using Comabit.DL.Data.Portfolio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.Inquiry
{
    public class Inquiry
    {
        [Key]
        public Guid Id { get; set; }

        public int InquiryNumber { get; set; }

        public string Purepose { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedByUserId { get; set; }

        public DateTime Deadline { get; set; }

        public string DeadlineInfo { get; set; }
        
        public string DeliveryPlace { get; set; }

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

        public ICollection<PortfolioCategory> PortfolioCategories { get; set; }

        public ICollection<PortfolioSubCategory> PortfolioSubCategories { get; set; }

        public ICollection<InquiryFile> Files { get; set; }

        public ICollection<Match.Match> Matches { get; set; }

        public ICollection<InquirySellerExclusion> ExcludedSellers { get; set; }

        public Inquiry()
        {
            this.PortfolioCategories = new HashSet<PortfolioCategory>();
            this.PortfolioSubCategories = new HashSet<PortfolioSubCategory>();
            this.Files = new HashSet<InquiryFile>();
            this.Matches = new HashSet<Match.Match>();
            this.ExcludedSellers = new HashSet<InquirySellerExclusion>();
        }
    }
}