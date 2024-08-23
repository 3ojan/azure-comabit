using Comabit.DL;

namespace Comabit.UI.Areas.Authentication.Models
{
    public class RegisterMailViewModel
    {
        public string FullName { get; set; }

        public string CallbackUrl { get; set; }

        public RegisterMailViewModel()
        {

        }

        public RegisterMailViewModel(string fullName, string callbackUrl)
        {
            FullName = fullName;
            CallbackUrl = callbackUrl;
        }
    }
}