// <copyright file="HomeViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Models
{
    using Comabit.UI.Areas.Authentication.Models;
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }
}
