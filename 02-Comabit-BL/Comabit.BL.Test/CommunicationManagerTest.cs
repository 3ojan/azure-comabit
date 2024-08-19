using Comabit.BL.Communication;
using Comabit.BL.Communication.DTO;
using Comabit.DL.DBDal.Services;
using Comabit.DL.Interfaces;
using Comabit.DL.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Comabit.BL.Test
{
    public class CommunicationManagerTest : BaseManagerTests
    {
        private CommunicationManager _cummunicationManager;
        private IMatchService _matchservice;
        private ICompanyService _companyservice;
        private IMailTemplateService _mailtemplateservice;

        [SetUp]
        public void Setup()
        {
            this._matchservice = new MatchService(this.UnitOfWork);
            this._companyservice = new CompanyService(this.UnitOfWork);
            this._mailtemplateservice = new MailTemplateService(this.UnitOfWork);
            this._cummunicationManager = new CommunicationManager(this._matchservice, this._companyservice, this._mailtemplateservice);
        }

        [Test]
        public void TestSendMail()
        {
            var mailRequest = new MailRequest()
            {
                ToEmail = "Einkauf04@comabit.de",
                ToName = "Max Mustermann",
                Subject = "Testmail",
                Body = "Das ist ein Test"
            };
            _cummunicationManager.SendEmail(mailRequest);
        }

        [Test]
        public async Task TestSendOfferMails()
        {
            //await _cummunicationManager.SendOfferMessageToSeller(Guid.Parse("c7263de7-cad5-4ffe-ad78-3495c2d11a2b"), "Ist noch Offen kein email");
            await _cummunicationManager.SendOfferMessageToSeller(Guid.Parse("45cffbca-2055-49b2-af6a-9f656d19ad96"), "Hiermit beauftrage ich Sie ......");
            await _cummunicationManager.SendOfferMessageToSeller(Guid.Parse("1a3e9cca-29b4-403e-b0a7-f1619f522529"), "Leider können wir Ihr Angebot nicht annehmen");
        }
    }
}
