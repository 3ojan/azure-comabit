using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Comabit.BL.ElasticSearch;
using Comabit.BL.Inquiry.Dto;
using Comabit.BL.Match;
using Comabit.BL.Porfolio.Dto;
using Comabit.BL.Shared;
using Comabit.DL.Data.File;
using Comabit.DL.Data.Inquiry;
using Comabit.DL.Data.Portfolio;
using Comabit.DL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Comabit.BL.Inquiry
{
    public class InquiryManager : BaseManager
    {
        private IInquiryService _inquiryService;
        private ICompanyService _companyService;
        private IPortfolioService _portfolioService;
        private IElasticSearchService _elasticSearchService;
        private IFileService _fileService;
        private ILogService _logService;
        private IMatchService _matchservice;
        private IInquirySellerExclusionService _inquirySellerExclusionService;

        public InquiryManager(IInquirySellerExclusionService inquirySellerExclusionService, ILogService logService, IInquiryService inquiryService, ICompanyService companyService, IPortfolioService portfolioService, IElasticSearchService elasticSearchService, IMatchService matchservice, IFileService fileservice)
        {
            this._inquiryService = inquiryService;
            this._companyService = companyService;
            this._portfolioService = portfolioService;
            this._matchservice = matchservice;
            this._elasticSearchService = elasticSearchService;
            this._fileService = fileservice;
            this._logService = logService;
            this._inquirySellerExclusionService = inquirySellerExclusionService;
        }

        public async Task<ICollection<Comabit.DL.Data.Inquiry.Inquiry>> GetInquiriesWithoutMatches()
        {
            return await this._inquiryService.GetAll().Where(i => i.IsPublished && !i.Matches.Any()).ToListAsync();
        }

        public async Task<ICollection<InquiryItem>> GetInquiries(Guid buyerId)
        {
            var inquiryEntity = await this._inquiryService.GetInquiries(buyerId).ToListAsync();

            return this.Mapper.Map<ICollection<InquiryItem>>(inquiryEntity);
        }

        public async Task<ICollection<InquiryItem>> GetInquiries(Guid buyerId, int maxCount, bool orderByUpdatedAt)
        {
            if (orderByUpdatedAt)
            {
                var inquiryEntity = await this._inquiryService.GetInquiries(buyerId).Take(maxCount).OrderByDescending(o => o.UpdatedAt).ToListAsync();
                return this.Mapper.Map<ICollection<InquiryItem>>(inquiryEntity);
            }
            else
            {
                var inquiryEntity = await this._inquiryService.GetInquiries(buyerId).Take(maxCount).OrderByDescending(o => o.CreatedAt).ToListAsync();
                return this.Mapper.Map<ICollection<InquiryItem>>(inquiryEntity);
            }
        }

        public async Task<ICollection<InquiryItem>> GeInquiriesNames(Guid buyerId)
        {
            var inquiryEntity = await this._inquiryService.GetInquiryNames(buyerId).OrderByDescending(i => i.InquiryNumber).ToListAsync();

            return this.Mapper.Map<ICollection<InquiryItem>>(inquiryEntity);
        }

        public async Task<ICollection<ProjectItem>> GetProjectNames(Guid buyerId)
        {
            var projectEntity = await this._inquiryService.GetProjectNames(buyerId).OrderByDescending(i => i.ProjectName).ToListAsync();

            return this.Mapper.Map<ICollection<ProjectItem>>(projectEntity);
        }

        public async Task<ICollection<InquiryItem>> GetInquiriesForProject(Guid projectId)
        {
            var inquiryEntity = await this._inquiryService.GetBuyerProjectInquiryById(projectId).ToListAsync();

            return this.Mapper.Map<ICollection<InquiryItem>>(inquiryEntity);
        }

        public async Task<IEnumerable<ProjectItem>> GetBuyerProjects(Guid buyerCompanyId, bool onlyActive = false)
        {
            var inquiryEntities = await this._inquiryService.GetBuyerProjectsByBuyerCompanyId(buyerCompanyId, onlyActive).ToListAsync();

            var result = inquiryEntities.Select(e => this.Mapper.Map<Project, ProjectItem>(e)).ToList();

            return result;
        }

        public async Task<ProjectItem> GetBuyerProjectById(Guid buyerProjectId)
        {
            var inquiryEntity = await this._inquiryService.GetBuyerProjectById(buyerProjectId).FirstOrDefaultAsync();

            var result = this.Mapper.Map<Project, ProjectItem>(inquiryEntity);

            return result;
        }

        public async Task CreateProject(ProjectItem project, string userId, Guid buyerId)
        {
            project.CreatedAt = DateTime.Now;
            project.UpdatedAt = DateTime.Now;
            project.CreatedByUserId = userId;
            project.UpdatedByUserId = userId;
            project.BuyerId = buyerId;
            project.IsActive = true;

            this._inquiryService.AddProject(this.Mapper.Map<Project>(project));
            await this._inquiryService.SaveAsync();


        }

        public async Task<InquiryItem> GetInquiryForEdit(Guid inquiryId)
        {
            InquiryItem inquiry = this.Mapper.Map<InquiryItem>(await this._inquiryService.GetInquiry(inquiryId).FirstOrDefaultAsync());

            if (inquiry.IsPublished)
            {
                inquiry.PublishState = PublishState.publish;
            }
            else if (inquiry.PublishedAt.HasValue)
            {
                inquiry.PublishState = PublishState.publishAt;
            }
            else
            {
                inquiry.PublishState = PublishState.draft;
            }

            inquiry.PortfolioAreas = this.Mapper.Map<ICollection<PortfolioAreaItem>>(await this._portfolioService.GetAllAreas().ToListAsync());

            foreach (PortfolioCategoryItem cateory in inquiry.PortfolioAreas.SelectMany(p => p.PortfolioCategories))
            {
                if (inquiry.PortfolioCategories.Any(c => c.Id == cateory.Id))
                {
                    cateory.Checked = true;
                }
            }

            foreach (PortfolioSubCategoryItem subCateory in inquiry.PortfolioAreas.SelectMany(p => p.PortfolioCategories.SelectMany(c => c.PortfolioSubCategories)))
            {
                if (inquiry.PortfolioSubCategories.Any(c => c.Id == subCateory.Id))
                {
                    subCateory.Checked = true;
                }
            }

            inquiry.ExcludedSellerIds = inquiry.ExcludedSellers.Select(e => e.SellerId).ToList();

            return inquiry;
        }

        public async Task<InquiryItem> GetInquiry(Guid inquiryId)
        {
            InquiryItem inquiry = this.Mapper.Map<InquiryItem>(await this._inquiryService.GetInquiry(inquiryId).FirstOrDefaultAsync());

            return inquiry;
        }

        public async Task CreateInquiry(InquiryItem inquiryItem, string userId)
        {
            DL.Data.Inquiry.Inquiry inquiry = this.Mapper.Map<DL.Data.Inquiry.Inquiry>(inquiryItem);

            if (inquiryItem.PublishState == PublishState.publish)
            {
                inquiry.IsPublished = true;
                inquiry.PublishedAt = DateTime.Now;
            }
            else if (inquiryItem.PublishState == PublishState.publishAt)
            {
                inquiry.IsPublished = inquiryItem.PublishedAt <= DateTime.Now;
                inquiry.PublishedAt = inquiryItem.PublishedAt;
            }
            else
            {
                inquiry.IsPublished = false;
                inquiry.PublishedAt = null;
            }

            inquiry.Id = Guid.NewGuid();
            inquiry.CreatedByUserId = userId;
            inquiry.CreatedAt = DateTime.Now;
            inquiry.UpdatedAt = DateTime.Now;
            inquiry.UpdatedByUserId = userId;

            inquiry.PortfolioCategories = await this.GetPortfolioCategories(inquiryItem.PortfolioAreas);
            inquiry.PortfolioSubCategories = await this.GetPortfolioSubCategories(inquiryItem.PortfolioAreas);

            foreach (IFormFile file in inquiryItem.UploadedFiles)
            {
                InquiryFile inquiryFile = new InquiryFile()
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    MimeType = file.ContentType,
                    Size = ((int)file.Length),
                    InquiryId = inquiry.Id,
                };

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    inquiryFile.FileData = new FileData()
                    {
                        Data = ms.ToArray(),
                        FileId = inquiryFile.Id,
                    };
                }

                inquiry.Files.Add(inquiryFile);

            }

            inquiry.ExcludedSellers = inquiryItem.ExcludedSellerIds.Select(s => new InquirySellerExclusion() { InquiryId = inquiry.Id, SellerId = s }).ToList();

            this._inquiryService.AddInquiry(inquiry);

            await this._inquiryService.SaveAsync();

            inquiry.Project = await this._inquiryService.GetBuyerProjectById(inquiry.ProjectId).FirstOrDefaultAsync();



            await this.StartSearchForMatches(this.Mapper.Map<InquiryItem>(inquiry));
        }

        public async Task CancelInquiry(Guid inquiryId, string userId)
        {
            var inquiry = await this._inquiryService.GetInquiry(inquiryId).FirstOrDefaultAsync();

            inquiry.UpdatedAt = DateTime.Now;
            inquiry.UpdatedByUserId = userId;
            inquiry.IsCanceled = true;

            foreach (var match in inquiry.Matches.Where(m => m.Offers.Any()))
            {
                foreach (var offer in match.Offers)
                {
                    if (offer.State == DL.Data.Match.OfferState.pending || offer.State == DL.Data.Match.OfferState.renew)
                    {
                        offer.State = DL.Data.Match.OfferState.revoked;
                    }
                }
            }

            await this._inquiryService.SaveAsync();

            await this.StartSearchForMatches(this.Mapper.Map<InquiryItem>(inquiry));
        }

        public async Task UpdateInquiryState(InquiryItem inquiryItem, string userId)
        {
            DL.Data.Inquiry.Inquiry inquiry = await this._inquiryService.GetInquiry(inquiryItem.Id).FirstOrDefaultAsync();

            inquiry.IsClosed = inquiryItem.IsClosed;
            inquiry.PlacingState = inquiryItem.PlacingState;
            inquiry.UpdatedAt = DateTime.Now;
            inquiry.UpdatedByUserId = userId;

            await this._inquiryService.SaveAsync();
        }

        public async Task UpdateInquiry(InquiryItem inquiryItem, string userId)
        {
            Comabit.DL.Data.Inquiry.Inquiry inquiry = await this._inquiryService.GetInquiry(inquiryItem.Id).FirstOrDefaultAsync();

            if (inquiryItem.PublishState == PublishState.publish)
            {
                inquiry.IsPublished = true;
                inquiry.PublishedAt = DateTime.Now;
            }
            else if (inquiryItem.PublishState == PublishState.publishAt)
            {
                inquiry.IsPublished = inquiryItem.PublishedAt <= DateTime.Now;
                inquiry.PublishedAt = inquiryItem.PublishedAt;
            }
            else
            {
                inquiry.IsPublished = false;
                inquiry.PublishedAt = inquiryItem.PublishedAt;
            }

            inquiry.AddidtionalTags = inquiryItem.AddidtionalTags;
            inquiry.Deadline = inquiryItem.Deadline;
            inquiry.DeadlineInfo = inquiryItem.DeadlineInfo;

            inquiry.DeliveryDate = inquiryItem.DeliveryDate;
            inquiry.DeliveryInfo = inquiryItem.DeliveryInfo;
            inquiry.DeliveryPostalCode = inquiryItem.DeliveryPostalCode;
            inquiry.DeliveryCity = inquiryItem.DeliveryCity;
            inquiry.DeliveryStreet = inquiryItem.DeliveryStreet;

            inquiry.Notes = inquiryItem.Notes;
            inquiry.Purepose = inquiryItem.Purepose;
            inquiry.Requirements = inquiryItem.Requirements;
            inquiry.UpdatedAt = DateTime.Now;
            inquiry.UpdatedByUserId = userId;

            inquiry.PortfolioCategories = await this.GetPortfolioCategories(inquiryItem.PortfolioAreas);
            inquiry.PortfolioSubCategories = await this.GetPortfolioSubCategories(inquiryItem.PortfolioAreas);

            foreach (FileItem fileItem in inquiryItem.Files.Where(f => f.Delete))
            {
                this._fileService.Delete(fileItem.Id);
            }

            foreach (IFormFile file in inquiryItem.UploadedFiles)
            {
                InquiryFile inquiryFile = new InquiryFile()
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    MimeType = file.ContentType,
                    Size = ((int)file.Length),
                    InquiryId = inquiry.Id,
                };

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    inquiryFile.FileData = new FileData()
                    {
                        Data = ms.ToArray(),
                        FileId = inquiryFile.Id,
                    };
                }

                this._inquiryService.AddInquiryFile(inquiryFile);
            }

            inquiry.ExcludedSellers = inquiryItem.ExcludedSellerIds.Select(s => new InquirySellerExclusion() { InquiryId = inquiry.Id, SellerId = s }).ToList();

            await this._inquiryService.SaveAsync();
            await this.StartSearchForMatches(this.Mapper.Map<InquiryItem>(inquiry));
        }

        public async Task PublishInquiry(Guid inquiryId, string userId)
        {
            Comabit.DL.Data.Inquiry.Inquiry inquiry = await this._inquiryService.GetInquiry(inquiryId).FirstOrDefaultAsync();

            inquiry.PublishedAt = DateTime.Now;
            inquiry.IsPublished = true;
            inquiry.UpdatedAt = DateTime.Now;
            inquiry.UpdatedByUserId = userId;

            await this._inquiryService.SaveAsync();

            await this.StartSearchForMatches(this.Mapper.Map<InquiryItem>(inquiry));
        }

        public async Task UpdateBuyerProject(ProjectItem projectItem)
        {
            var projectEditItem = this.Mapper.Map<ProjectItem, ProjectEditItem>(projectItem);
            var projectEntity = await this._inquiryService.GetBuyerProjectById(projectItem.Id).SingleOrDefaultAsync();

            this.Mapper.Map<ProjectEditItem, Project>(projectEditItem, projectEntity);

            this._inquiryService.UpdateBuyerProject(projectEntity);

            await this._inquiryService.SaveAsync();
        }

        private async Task<ICollection<PortfolioCategory>> GetPortfolioCategories(ICollection<PortfolioAreaItem> portfolioAreas)
        {
            ICollection<PortfolioCategory> portfolioCategories = new List<PortfolioCategory>();

            if (portfolioAreas != null && portfolioAreas.Any(a => a.PortfolioCategories.Any(c => c.Checked)))
            {
                List<Guid> categoryIds = new List<Guid>();

                foreach (PortfolioCategoryItem categoryItem in portfolioAreas.SelectMany(a => a.PortfolioCategories.Where(c => c.Checked).ToList()).ToList())
                {
                    categoryIds.Add(categoryItem.Id);
                }

                portfolioCategories = await this._portfolioService.GetCategoriesByIds(categoryIds).ToListAsync();
            }

            return portfolioCategories;
        }

        private async Task<ICollection<PortfolioSubCategory>> GetPortfolioSubCategories(ICollection<PortfolioAreaItem> portfolioAreas)
        {
            ICollection<PortfolioSubCategory> portfolioSubCategories = new List<PortfolioSubCategory>();

            if (portfolioAreas != null)
            {
                if (portfolioAreas.Any(a => a.PortfolioCategories.Any(c => c.PortfolioSubCategories.Any(s => s.Checked))))
                {
                    List<Guid> subCategoryIds = new List<Guid>();

                    foreach (Guid subCategoryId in portfolioAreas.SelectMany(a => a.PortfolioCategories.SelectMany(c => c.PortfolioSubCategories)).Where(c => c.Checked).Select(c => c.Id).ToList())
                    {
                        subCategoryIds.Add(subCategoryId);
                    }

                    portfolioSubCategories = await this._portfolioService.GetSubCategoriesByIds(subCategoryIds).ToListAsync();
                }
            }

            return portfolioSubCategories;
        }

        public async Task StartSearchForMatches(InquiryItem inquiry)
        {
            if (inquiry != null)
            {
                if (inquiry.IsPublished == true && inquiry.PublishedAt <= DateTime.Now)
                {
                    var matchManager = new MatchManager(this._matchservice, this._companyService, this._elasticSearchService, this._logService);

                    await matchManager.CreateMatchesForInquiry(inquiry);
                }
            }
        }

        public async Task<ICollection<DL.Data.Inquiry.Inquiry>> GetInquiriesToPublish()
        {
            return await this._inquiryService.GetQueryable()
                .Include(i => i.PortfolioCategories)
                .Include(i => i.PortfolioSubCategories)
                .Where(i => !i.IsPublished && i.PublishedAt.HasValue && i.PublishedAt.Value <= DateTime.Now).ToListAsync();
        }

        public async Task<int> PublishInquiries(ICollection<DL.Data.Inquiry.Inquiry> inquiries)
        {
            int publishedInquiryCounter = 0;

            foreach (var inquiry in inquiries)
            {
                inquiry.IsPublished = true;

                publishedInquiryCounter += await this._inquiryService.SaveAsync();

                await StartSearchForMatches(this.Mapper.Map<InquiryItem>(inquiry));
            }

            return publishedInquiryCounter;
        }
    }
}