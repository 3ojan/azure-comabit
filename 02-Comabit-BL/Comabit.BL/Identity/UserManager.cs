// <copyright file="UserManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Identity
{
    using Comabit.BL.Company;
    using Comabit.BL.Identity.Dto;
    using Comabit.BL.Porfolio.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL;
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.Portfolio;
    using Comabit.DL.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    public class UserManager : BaseManager
    {
        private UserManager<ApplicationUser> _userManager;

        private RoleManager<ApplicationRole> _roleManager;

        private ICompanyService _companyService;

        private IPortfolioService _portfolioService;

        private readonly SellerCompanyManager _sellerCompanyManager;

        public UserManager(SellerCompanyManager sellerCompanyManager, IPortfolioService portfolioService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ICompanyService companyService)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._companyService = companyService;
            this._portfolioService = portfolioService;
            this._sellerCompanyManager = sellerCompanyManager;
        }

        public async Task<ApplicationUser> RegisterSeller(RegisterSellerItem sellerItem)
        {
            ApplicationUser user = this.CreateUser(sellerItem);

            IdentityResult result = await _userManager.CreateAsync(user, sellerItem.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.Seller);

                if (result.Succeeded)
                {
                    await this._sellerCompanyManager.CreateSellerCompany(sellerItem, user);
                }
            }
            else
            {
                throw new Exception(result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b));
            }

            return user;
        }

        public async Task<ApplicationUser> RegisterBuyer(RegisterBaseItem registerItem)
        {
            ApplicationUser user = this.CreateUser(registerItem);

            IdentityResult result = await _userManager.CreateAsync(user, registerItem.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.Buyer);

                if (result.Succeeded)
                {
                    await this._sellerCompanyManager.CreateBuyerCompany(registerItem, user);
                }
            }
            else
            {
                throw new Exception(result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b));
            }

            return user;
        }

        public async Task<ICollection<string>> GetRole(ApplicationUser user)
        {
            return await this._userManager.GetRolesAsync(user);
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            return await this._userManager.Users.OrderBy(u => u.UserName).ToListAsync();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        private ApplicationUser CreateUser(RegisterBaseItem item)
        {
            return new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = item.EMail,
                Email = item.EMail,
                CreatedAt = DateTime.Now,
                EmailConfirmed = false,
                Firstname = item.ContactPersonFirstname,
                Lastname = item.ContactPersonLastname,
            };
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string passwordHash, string newPassword)
        {
            return this._userManager.PasswordHasher.VerifyHashedPassword(user, passwordHash, newPassword);
        }

        public async Task<IdentityResult> Update(ApplicationUser user)
        {
            return await this._userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await this._userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<ApplicationUser> GetUserByEMail(string eMail)
        {
            return await _userManager.FindByEmailAsync(eMail);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser user, RoleManager<ApplicationRole> roleManager, IPermissionService permissionService, bool isPersistent = false)
        {
            if (string.IsNullOrEmpty(user.ClaimsSecurityStamp))
            {
                user.ClaimsSecurityStamp = Guid.NewGuid().ToString();

                await _userManager.UpdateAsync(user);
            }

            ClaimsIdentity userIdentity = new ClaimsIdentity(await _userManager.GetClaimsAsync(user));
            
            if (await _userManager.IsInRoleAsync(user, Roles.Seller))
            {
                Seller seller = _companyService.GetSeller(user.Id);
                
                if (seller != null)
                {
                    userIdentity.AddClaim(new Claim(ComabitClaimTypes.CompanyGuid, seller.Id.ToString()));
                    userIdentity.AddClaim(new Claim(ComabitClaimTypes.CompanyName, seller.Name));
                }
            }
            else if (await _userManager.IsInRoleAsync(user, Roles.Buyer))
            {
                Buyer buyer = _companyService.GetBuyerByUserId(user.Id);
                
                if (buyer != null) { 
                    userIdentity.AddClaim(new Claim(ComabitClaimTypes.CompanyGuid, buyer.Id.ToString()));
                    userIdentity.AddClaim(new Claim(ComabitClaimTypes.CompanyName, buyer.Name));
                }
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);
            ApplicationRole role = await roleManager.FindByNameAsync(roles.FirstOrDefault());

            var permissions = permissionService.GetPermissionForRole(role.Id);
            var roleClaims = ClaimsRoles.CreateRoleClaims(user, role, permissionService);

            userIdentity.AddClaims(roleClaims);
            userIdentity.AddClaim(new Claim(ComabitClaimTypes.ClaimSecurityStamp, user.ClaimsSecurityStamp));
            userIdentity.AddClaim(new Claim(ComabitClaimTypes.RememberMe, isPersistent.ToString()));

            return userIdentity;
        }

        public async Task<ICollection<ApplicationUser>> GetAllAdmins()
        {
            return await this._userManager.GetUsersInRoleAsync(Roles.Admin);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password)
        {
            return await _userManager.ResetPasswordAsync(user, code, password);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}