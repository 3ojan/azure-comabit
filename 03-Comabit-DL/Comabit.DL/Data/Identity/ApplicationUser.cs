namespace Comabit.DL
{
    using Comabit.DL.Data.Company;
    using Comabit.DL.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IPermissionService permissionService, bool isPersistent = false)
        {
            if (string.IsNullOrEmpty(this.ClaimsSecurityStamp))
            {
                this.ClaimsSecurityStamp = Guid.NewGuid().ToString();

                await userManager.UpdateAsync(this);
            }

            ClaimsIdentity userIdentity = new ClaimsIdentity(await userManager.GetClaimsAsync(this));

            Company company = userManager.Users.Include(u => u.Company).Where(u => u.Id == this.Id).SingleOrDefault().Company;

            if (company != null)
            {
                userIdentity.AddClaim(new Claim(ComabitClaimTypes.CompanyGuid, company.Id.ToString()));
                userIdentity.AddClaim(new Claim(ComabitClaimTypes.CompanyName, company.Name));
            }

            IList<string> roles = await userManager.GetRolesAsync(this);
            ApplicationRole role = await roleManager.FindByNameAsync(roles.FirstOrDefault());

            var permissions = permissionService.GetPermissionForRole(role.Id);
            var roleClaims = ClaimsRoles.CreateRoleClaims(this, role, permissionService);

            userIdentity.AddClaims(roleClaims);
            userIdentity.AddClaim(new Claim(ComabitClaimTypes.ClaimSecurityStamp, this.ClaimsSecurityStamp));
            userIdentity.AddClaim(new Claim(ComabitClaimTypes.RememberMe, isPersistent.ToString()));

            return userIdentity;
        }

        public static ApplicationUser CreateNewApplicationUserByEmail(string email)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = email,
                EmailConfirmed = true,
                CreatedAt = DateTime.Now,
            };

            return user;
        }

        public static ApplicationUser CreateNewApplicationUserByName(string name)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = name,
                EmailConfirmed = true,
                CreatedAt = DateTime.Now,
            };

            return user;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime PasswordChangeDate { get; set; }

        public string ClaimsSecurityStamp { get; set; }

        public DateTime? PasswordSentDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }

        public string FullName
        {
            get
            {
                return !string.IsNullOrEmpty(Firstname) ? Firstname + @" " + Lastname : Lastname;
            }
        }

        public string LastNameAndFirstname
        {
            get
            {
                return !string.IsNullOrEmpty(Lastname) ? Lastname + @", " + Firstname : "";
            }
        }

        public Company Company { get; set; }
    }
}