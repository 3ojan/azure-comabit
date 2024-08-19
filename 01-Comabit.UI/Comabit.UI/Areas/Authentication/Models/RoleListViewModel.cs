// <copyright file="RoleListViewModel.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Areas.Authentication.Models
{
    public class RoleListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PermissionsCount { get; set; }

        public bool IsProtected
        {
            get
            {
                return Name == "SuperAdmin" || Name == "Admin" || Name == "Teamleiter" || Name == "Agent";
            }
        }
    }
}