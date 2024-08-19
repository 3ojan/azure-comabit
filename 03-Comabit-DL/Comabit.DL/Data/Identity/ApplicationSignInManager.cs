namespace Comabit.DL
{
    using Comabit.DL.Interfaces;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private UserManager<ApplicationUser> userManager { get; set; }

        private IPermissionService permissionService { get; set; }

        public ApplicationSignInManager(IPermissionService permissionService, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) 
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            this.userManager = userManager;
            this.permissionService = permissionService;
        }

        public override Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod)
        {
            return base.SignInAsync(user, isPersistent);
        }

        public override Task RefreshSignInAsync(ApplicationUser user)
        {
            return base.RefreshSignInAsync(user);
        }
    }
}
