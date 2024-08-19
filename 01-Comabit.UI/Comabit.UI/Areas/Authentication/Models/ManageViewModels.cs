// <copyright file="EditProfileViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL;
using System.ComponentModel.DataAnnotations;

namespace Comabit.UI.Areas.Authentication.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class EditProfileViewModel
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Company { get; set; }
        
        public bool ChangePassword { get; set; }

        // [RequiredIf("ChangePassword", true)]
        [Display(Name = "Aktuelles Passwort")]
        public string OldPassword { get; set; }
        
        // [RequiredIf("ChangePassword", true)]
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [Display(Name = "Neues Passwort")]
        public string NewPassword { get; set; }

        // [RequiredIf("ChangePassword", true)]
        [Display(Name = "Passwort Bestätigung")]
        [Compare("NewPassword", ErrorMessage = "Die beiden neuen Passwörter stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }
    }
}