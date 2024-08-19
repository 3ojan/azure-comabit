// <copyright file="EditUserViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.DL;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string SelectedRole { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

        public EditUserViewModel()
        {

        }

        public EditUserViewModel(ApplicationUser user, IList<string> userRoles, List<ApplicationRole> roles)
        {
            Id = user.Id;
            Email = user.Email;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            SelectedRole = userRoles.FirstOrDefault();
            RolesList = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name
            });
        }
    }
}