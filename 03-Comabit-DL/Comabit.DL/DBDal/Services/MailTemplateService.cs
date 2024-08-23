// <copyright file="MailTemplateService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.DBDal.Services
{
    using Comabit.DL.Data.Templates;
    using Comabit.DL.Interfaces;
    using Comabit.DL.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MailTemplateService : IMailTemplateService
    {
        private IUnitOfWork unitOfWork;
        private IGenericRepository<MailTemplate> _mailTemplateRepository;

        public MailTemplateService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this._mailTemplateRepository = new GenericRepository<MailTemplate>(this.unitOfWork.DbContext);
        }

        public IQueryable<MailTemplate> GetByTemplateName(string templateName) 
        {
            return this._mailTemplateRepository.GetAll().Where(o => o.TemplateName.Equals(templateName));
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}
