// <copyright file="ListUsersViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    using Comabit.DL;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ListUsersViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }
}