using Comabit.BL.Mail.DTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Configuration;
using System.Threading.Tasks;

namespace Comabit.BL.Mail
{
    public class MailManager
    {
        private readonly MailSettings _mailSettings;

        public MailManager(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));

            if (mailRequest.ToEmail.Contains(";"))
            {
                var mailAddresses = mailRequest.ToEmail.Split(';');

                foreach (string mailAddress in mailAddresses)
                {
                    email.To.Add(MailboxAddress.Parse(mailAddress));
                }
            }
            else
            {
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            }

            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
