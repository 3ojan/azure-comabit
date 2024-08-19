// <copyright file="IInquiryService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.File;
    using Comabit.DL.Data.Inquiry;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IInquiryService : IAsyncDisposable
    {
        IQueryable<Project> GetAllBuyerProjects();

        IQueryable<Inquiry> GetAll();

        IQueryable<Inquiry> GetQueryable();

        IQueryable<Project> GetBuyerProjectsByBuyerCompanyId(Guid buyerCompanyId, bool onlyActive);

        IQueryable<Project> GetBuyerProjectById(Guid projectId);

        IQueryable<Inquiry> GetInquiries(Guid buyerId);

        IQueryable<Inquiry> GetInquiryNames(Guid buyerId);

        IQueryable<Project> GetProjectNames(Guid buyerId);

        IQueryable<Inquiry> GetInquiry(Guid inquiryId);

        void AddProject(Project project);

        void UpdateBuyerProject(Project buyerProject);

        IQueryable<Inquiry> GetBuyerProjectInquiryById(Guid projectId);

        void AddInquiry(Inquiry inquiry);

        void UpdateBuyerProjectInquiry(Inquiry buyerProjectInquiry);

        IQueryable<InquiryFile> GetInquiryFileById(Guid id);

        void AddInquiryFile(InquiryFile inquiryFile);

        void DeleteInquiryFile(InquiryFile inquiryFile);

        Task<int> SaveAsync();
    }
}