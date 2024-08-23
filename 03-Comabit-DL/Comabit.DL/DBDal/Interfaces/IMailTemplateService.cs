// <copyright file="IMailTemplateService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Templates;
    using System;
    using System.Linq;

    public interface IMailTemplateService : IAsyncDisposable
    {
        IQueryable<MailTemplate> GetByTemplateName(string templateName);
    }
}
