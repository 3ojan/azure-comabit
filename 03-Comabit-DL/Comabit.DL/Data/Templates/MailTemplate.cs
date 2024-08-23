using System;
using System.ComponentModel.DataAnnotations;

namespace Comabit.DL.Data.Templates
{
    public class MailTemplate
    {
        [Key]
        public Guid Id { get; set; }

        public string TemplateName { get; set; }

        public string Subject { get; set; }

        public string Code { get; set; }
    }
}
