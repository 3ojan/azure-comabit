// <copyright file="CreateUserViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passwort Bestätigung")]
        [Compare("Password", ErrorMessage = "Die beiden Passwörter stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }

        public string Firstname { get; set; }
        
        public string Lastname { get; set; }

        public string SelectedRole { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}