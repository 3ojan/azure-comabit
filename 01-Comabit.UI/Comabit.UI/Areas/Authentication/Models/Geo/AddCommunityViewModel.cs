using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Authentication.Models.Geo
{
    public class AddCommunityViewModel
    {
        public ICollection<CommunityViewModel> Communities { get; set; }

        public Guid NewCommunityId { get; set; }
    }
}