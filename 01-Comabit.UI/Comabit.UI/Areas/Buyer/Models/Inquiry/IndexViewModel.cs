// <copyright file="IndexViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Buyer.Models.Inquiry
{
    using Comabit.UI.Models.Match;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IndexViewModel
    {
        public ICollection<InquiryViewModel> Inquiries { get; set; }

        public ICollection<ProjectViewModel> Projects { get; set; }

        private ProjectViewModel _SelectedProject { get; set; }

        public ProjectViewModel SelectedProject
        {
            get
            {
                if (_SelectedProject == null && SelectedProjectId.HasValue)
                {
                    _SelectedProject = Projects.Where(p => p.Id == SelectedProjectId.Value).FirstOrDefault();
                }

                return _SelectedProject;
            }
        }

        public Guid? SelectedProjectId { get; set; }

        public IndexViewModel()
        {
            this.Projects = new HashSet<ProjectViewModel>();
            this.Inquiries = new HashSet<InquiryViewModel>();
        }

        public IndexViewModel(ICollection<InquiryViewModel> inquiries, ICollection<ProjectViewModel> projects)
        {
            this.Inquiries = inquiries;
            this.Projects = projects;
        }
    }
}