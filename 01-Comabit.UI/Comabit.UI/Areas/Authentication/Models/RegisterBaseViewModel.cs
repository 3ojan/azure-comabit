// <copyright file="RegisterViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.DL;
    using Comabit.DL.Data.Company;
    using Comabit.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterBaseViewModel
    {
        [Required]
        [RegularExpression(Validation.IsEmailValidRegEx)]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password wiederholen")]
        [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string Company { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string Street { get; set; }

        [Required]
        [RegularExpression(Validation.IsPostalcodeDeOrAtValidRegEx)]
        public string PostalCode { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string City { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string BusinessTaxId { get; set; }

        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string UstId { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string ContactPersonFirstname { get; set; }

        [Required]
        [RegularExpression(Validation.IsAlphaNumericValidRegEx)]
        public string ContactPersonLastname { get; set; }

        public ApplicationUser CreateUser()
        {
            return new ApplicationUser()
            {
                UserName = EMail,
                Email = EMail,
                Firstname = ContactPersonFirstname,
                Lastname = ContactPersonLastname,
                Company = new Company()
                {
                    Id = Guid.NewGuid(),
                    Street = Street,
                    PostalCode = PostalCode,
                    City = City,
                    BusinessTaxId = BusinessTaxId,
                    Name = Company,
                    UstId = UstId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            };
        }
    }
}