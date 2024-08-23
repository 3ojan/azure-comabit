namespace Comabit.DL
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.File;
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Log;
    using Comabit.DL.Data.Match;
    using Comabit.DL.Data.Portfolio;
    using Comabit.DL.Data.Templates;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<ApplicationPermission> Permissions { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Seller> Sellers { get; set; }

        public virtual DbSet<Buyer> Buyers{ get; set; }

        public virtual DbSet<PortfolioArea> PortfolioAreas { get; set; }

        public virtual DbSet<PortfolioCategory> PortfolioCategories { get; set; }

        public virtual DbSet<PortfolioSubCategory> PortfolioSubCategories { get; set; }

        public virtual DbSet<AdditionalPortfolioCategoryTags> AdditionalPortfolioCategoryTags { get; set; }

        public virtual DbSet<State> States { get; set; }

        public virtual DbSet<Province> Provinces { get; set; }

        public virtual DbSet<Community> Communities { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Inquiry> Inquiries { get; set; }

        public virtual DbSet<Match> Matches { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<UserMessage> UserMessages { get; set; }

        public virtual DbSet<SystemMessage> SystemMessages { get; set; }

        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<FileData> FileDatas { get; set; }

        public virtual DbSet<OfferFile> OfferFiles { get; set; }

        public virtual DbSet<InquiryFile> InquiryFiles { get; set; }

        public virtual DbSet<MailTemplate> MailTemplates { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        public virtual DbSet<InquirySellerExclusion> InquirySellerExclusions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateCompanyModel(modelBuilder);
            CreateGeoModel(modelBuilder);
            CreateSellerCompanyTagsModel(modelBuilder);
            CrateInquiryModel(modelBuilder);
            CreateFileModel(modelBuilder);
            CreateOfferModel(modelBuilder);
            CreateMatchModel(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateCompanyModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(m => m.Users).WithOne(m => m.Company).OnDelete(DeleteBehavior.Cascade);
        }

        private void CreateGeoModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasMany(m => m.Provinces).WithOne(m => m.State).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Province>().HasMany(m => m.Communities).WithOne(m => m.Province).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Community>().HasMany(m => m.Cities).WithOne(m => m.Community).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<City>().HasOne(m => m.Community);
        }

        private void CreateSellerCompanyTagsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seller>().HasMany(m => m.PortfolioCategories).WithMany(m => m.SellerCompanies);
            modelBuilder.Entity<PortfolioCategory>().HasMany(m => m.SellerCompanies).WithMany(m => m.PortfolioCategories);

            modelBuilder.Entity<Seller>().HasMany(m => m.PortfolioSubCategories).WithMany(m => m.SellerCompanies);
            modelBuilder.Entity<PortfolioSubCategory>().HasMany(m => m.SellerCompanies).WithMany(m => m.PortfolioSubCategories);

            modelBuilder.Entity<Seller>().HasMany(m => m.AdditionalPortfolioCategoryTags).WithOne(m => m.SellerCompany).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AdditionalPortfolioCategoryTags>(m => m.HasIndex(e => new { e.PortfolioCategoryId, e.SellerCompanyId }).IsUnique(true));

            // TODO: DB-Migration durchführen
            //modelBuilder.Entity<SellerCategory>(m => m.HasKey(e => new { e.PortfolioCategory, e.SellerId }));
            //modelBuilder.Entity<SellerCategory>(m => m.HasIndex(e => new { e.PortfolioCategory, e.SellerId }).IsUnique(true));
            //modelBuilder.Entity<SellerCategory>().HasOne(s => s.Seller).WithMany(s => s.Categories);
            //modelBuilder.Entity<SellerCategory>().HasOne(s => s.PortfolioCategory).WithMany(s => s.Sellers);
        }

        private void CrateInquiryModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>().HasMany(m => m.Projects).WithOne(m => m.Buyer).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>().HasMany(m => m.Inquiries).WithOne(m => m.Project).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Inquiry>().HasAlternateKey(m => m.InquiryNumber);
            modelBuilder.Entity<Inquiry>().Property(m => m.InquiryNumber).ValueGeneratedOnAdd();
            modelBuilder.Entity<Inquiry>().HasMany(m => m.PortfolioCategories).WithMany(m => m.BuyerProjectInquire);
            modelBuilder.Entity<Inquiry>().HasMany(m => m.PortfolioSubCategories).WithMany(m => m.BuyerProjectInquire);

            modelBuilder.Entity<Inquiry>().HasMany(m => m.Files).WithOne(m => m.Inquiry).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InquirySellerExclusion>(m => m.HasKey(e => new { e.InquiryId, e.SellerId }));
            modelBuilder.Entity<InquirySellerExclusion>(m => m.HasIndex(e => new { e.InquiryId, e.SellerId }).IsUnique(true));
            modelBuilder.Entity<InquirySellerExclusion>().HasOne(s => s.Seller).WithMany(s => s.InquiryExclusions);
            modelBuilder.Entity<InquirySellerExclusion>().HasOne(s => s.Inquiry).WithMany(s => s.ExcludedSellers);
        }

        private void CreateOfferModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>().HasOne(m => m.File).WithOne(m => m.Offer).OnDelete(DeleteBehavior.Cascade);
        }

        private void CreateFileModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().HasOne(m => m.FileData).WithOne(m => m.File).OnDelete(DeleteBehavior.Cascade);
        }

        private void CreateMatchModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasOne(m => m.Inquiry).WithMany(m => m.Matches).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Match>().HasOne(m => m.Seller).WithMany(m => m.Matches).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Match>().HasMany(m => m.Offers).WithOne(m => m.Match).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Match>().HasMany(m => m.Messages).WithOne(m => m.Match).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Match>(m => m.HasIndex(e => new { e.InquiryId, e.SellerId }).IsUnique());

            modelBuilder.Entity<PendingReading>(m => m.HasKey(e => new { e.CompanyId, e.MessageId }));
            modelBuilder.Entity<PendingReading>(m => m.HasIndex(e => new { e.CompanyId, e.MessageId }).IsUnique(true));
            modelBuilder.Entity<PendingReading>().HasOne(s => s.Company).WithMany(s => s.PendingReadings);
            modelBuilder.Entity<PendingReading>().HasOne(s => s.Message).WithMany(s => s.PendingReadings);
        }
    }
}