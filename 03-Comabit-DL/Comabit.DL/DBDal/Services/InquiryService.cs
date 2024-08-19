// <copyright file="PortfolioService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.File;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Portfolio;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InquiryService : IInquiryService
    {
        private IUnitOfWork unitOfWork;
        private readonly IGenericRepository<Inquiry> _projectInquiryRepository;
        private readonly IGenericRepository<Project> _buyerProjectRepository;
        private readonly IGenericRepository<InquiryFile> _inquiryFileRepository;

        public InquiryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this._projectInquiryRepository = new GenericRepository<Inquiry>(this.unitOfWork.DbContext);

            this._buyerProjectRepository = new GenericRepository<Project>(this.unitOfWork.DbContext);

            this._inquiryFileRepository = new GenericRepository<InquiryFile>(this.unitOfWork.DbContext);
        }

        public IQueryable<Project> GetAllBuyerProjects()
        {
            return this._buyerProjectRepository.GetAll(includeProperties:
                "Buyer,Inquiries," +
                "Inquiries.PortfolioSubCategories,Inquiries.PortfolioSubCategories.PortfolioCategory,Inquiries.PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "Inquiries.PortfolioCategories,Inquiries.PortfolioCategories.PortfolioArea");
        }

        public IQueryable<Inquiry> GetAll()
        {
            return this._projectInquiryRepository.GetAll(includeProperties:
                "PortfolioSubCategories,PortfolioSubCategories.PortfolioCategory,PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "PortfolioCategories,PortfolioCategories.PortfolioArea," +
                "Project,Project.Buyer,Matches");
        }

        public IQueryable<Project> GetBuyerProjectsByBuyerCompanyId(Guid buyerCompanyId, bool onlyActive = false)
        {
            IQueryable<Project> query = this._buyerProjectRepository.GetAll(includeProperties: "Buyer,Inquiries").Where(o => o.BuyerId == buyerCompanyId);

            if (onlyActive)
            {
                query = query.Where(q => q.IsActive);
            }

            return query;
        }

        public IQueryable<Project> GetBuyerProjectById(Guid projectId)
        {
            return this._buyerProjectRepository.GetAll(includeProperties: "Buyer,Inquiries,Inquiries.Matches,Inquiries.Matches.Offers").Where(o => o.Id == projectId);
        }

        public IQueryable<Inquiry> GetInquiries(Guid buyerId)
        {
            return this._projectInquiryRepository.GetAll(includeProperties:
                "PortfolioSubCategories,PortfolioSubCategories.PortfolioCategory,PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "PortfolioCategories,PortfolioCategories.PortfolioArea," +
                "Project,Project.Buyer,Matches,Matches.Offers").Where(i => i.Project.BuyerId == buyerId);
        }

        public IQueryable<Inquiry> GetInquiryNames(Guid buyerId)
        {
            return this._projectInquiryRepository.GetAll(includeProperties: "Project,Matches,Matches.Offers").Where(i => i.Project.BuyerId == buyerId);
        }

        public IQueryable<Project> GetProjectNames(Guid buyerId)
        {
            return this._buyerProjectRepository.GetAll(includeProperties: "Inquiries,Inquiries.Matches,Inquiries.Matches.Offers").Where(i => i.BuyerId == buyerId);
        }

        public IQueryable<Inquiry> GetInquiry(Guid inquiryId)
        {
            return this._projectInquiryRepository.GetAll(includeProperties:
                "PortfolioSubCategories,PortfolioSubCategories.PortfolioCategory,PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "PortfolioCategories,PortfolioCategories.PortfolioArea," +
                "Project,Project.Buyer,Matches,Matches.Offers,Files,ExcludedSellers").Where(i => i.Id == inquiryId);
        }

        public void AddProject(Project project)
        {
            this._buyerProjectRepository.Add(project);
        }

        public void UpdateBuyerProject(Project buyerProject)
        {
            this._buyerProjectRepository.Update(buyerProject);
        }

        public IQueryable<Inquiry> GetBuyerProjectInquiryById(Guid projectId)
        {
            return this._projectInquiryRepository.GetAll(includeProperties:
                "PortfolioSubCategories,PortfolioSubCategories.PortfolioCategory,PortfolioSubCategories.PortfolioCategory.PortfolioArea," +
                "PortfolioCategories,PortfolioCategories.PortfolioArea," +
                "Project,Project.Buyer,Matches,Matches.Offers").Where(i => i.ProjectId == projectId);
        }

        public void AddInquiry(Inquiry buyerProjectInquiry)
        {
            this._projectInquiryRepository.Add(buyerProjectInquiry);
        }

        public void UpdateBuyerProjectInquiry(Inquiry buyerProjectInquiry)
        {
            this._projectInquiryRepository.Update(buyerProjectInquiry);
        }

        public IQueryable<InquiryFile> GetInquiryFileById(Guid id)
        {
            return this._inquiryFileRepository.GetAll(includeProperties: "FileData").Where(f => f.Id == id);
        }

        public void AddInquiryFile(InquiryFile inquiryFile)
        {
            this._inquiryFileRepository.Add(inquiryFile);
        }

        public void DeleteInquiryFile(InquiryFile inquiryFile)
        {
            this._inquiryFileRepository.Delete(inquiryFile);
        }

        public IQueryable<Inquiry> GetQueryable()
        {
            return this._projectInquiryRepository.GetAll();
        }

        public async Task<int> SaveAsync()
        {
            return await unitOfWork.SaveAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}