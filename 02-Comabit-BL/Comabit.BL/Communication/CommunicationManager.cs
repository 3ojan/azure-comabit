using Comabit.BL.Communication.DTO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using Microsoft.AspNetCore.Razor;
using Comabit.DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Comabit.DL.Data.Match;
using Comabit.DL;
using Comabit.DL.Services;
using System.Linq;

namespace Comabit.BL.Communication
{
    public class CommunicationManager
    {
        private readonly MailSettings _mailSettings = new MailSettings()
        {
            Mail = "service@comabit.de",
            DisplayName = "Comabit Baustoffmanager",
            UserName = "comabit-de-0009",
            Password = "KNo!yic1ov#gnu3hw!as5mt6bi",
            Host = "smtps.udag.de",
            Port = 587
        };

        private IMatchService _matchservice;
        private ICompanyService _companyservice;
        private IMailTemplateService _mailtemplateservice;

        public CommunicationManager(IMatchService matchservice, ICompanyService companyservice, IMailTemplateService mailtemplateservice)
        {
            this._matchservice = matchservice;
            this._companyservice = companyservice;
            this._mailtemplateservice = mailtemplateservice;
        }

        public async Task SendOfferMessageToSeller(Guid offerId, string message)
        {
            try
            {
                var offer = await this._matchservice.GetOffer(offerId).SingleOrDefaultAsync();
                if (offer != null && offer.State != OfferState.pending)
                {
                    var matchEntity = await this._matchservice.GetMatchWithSellerCompany(offer.MatchId).SingleOrDefaultAsync();
                    var company = this._companyservice.GetCompany(matchEntity.SellerId);
                    var mainUser = company.Users.Where(o => o.Id.Equals(company.MainUserId)).SingleOrDefault();
                    if (mainUser != null)
                    {
                        var mailRequest = new MailRequest()
                        {
                            ToEmail = mainUser.Email,
                            ToName = mainUser.FullName
                        };
                        
                        var templateName = string.Empty;
                        switch (offer.State)
                        {
                            case OfferState.revoked:
                                templateName = "OfferRevoked";
                                break;

                            case OfferState.ordered:
                                templateName = "OfferOrdered";
                                break;

                            case OfferState.renew:
                                templateName = "OfferRenew";
                                break;
                        }
                        var mailTemplate = await this._mailtemplateservice.GetByTemplateName(templateName).SingleOrDefaultAsync();

                        if (mailTemplate != null)
                        {
                            mailRequest.Subject = this.ReplaceVariables(offer, matchEntity, company, message, mailTemplate.Subject);
                            mailRequest.Body = this.ReplaceVariables(offer, matchEntity, company, message, mailTemplate.Code);
                            await this.SendEmailAsync(mailRequest);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task SendOfferMessageToSeller(Guid offerId, Guid buyerId, string message)
        {
            await this.SendOfferMessageToSeller(offerId, message);
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
                    email.To.Add(new MailboxAddress(mailRequest.ToName, mailRequest.ToEmail));
                }
            }
            else
            {
                email.To.Add(new MailboxAddress(mailRequest.ToName, mailRequest.ToEmail));
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

        public void SendEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));

            if (mailRequest.ToEmail.Contains(";"))
            {
                var mailAddresses = mailRequest.ToEmail.Split(';');

                foreach (string mailAddress in mailAddresses)
                {
                    email.To.Add(new MailboxAddress(mailRequest.ToName, mailRequest.ToEmail));
                }
            }
            else
            {
                email.To.Add(new MailboxAddress(mailRequest.ToName, mailRequest.ToEmail));
            }

            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        private string ReplaceVariables(Offer offer, DL.Data.Match.Match matchEntity, DL.Data.Company.Company company, string message, string content)
        {
            var mainUser = company.Users.Where(o => o.Id.Equals(company.MainUserId)).SingleOrDefault();
            return content.Replace("@FirstName", mainUser.Firstname)
                            .Replace("@FullName", mainUser.FullName)
                            .Replace("@Message", message);
        }
    }
}