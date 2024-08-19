using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Authentication.Models.Geo
{
    [BindProperties]
    public class CommunityViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}