using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Areas.Authentication.Models.Geo
{
    [Serializable]
    public class StateViewModel
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<CommunityViewModel> Communities
        {
            get;
            set;
        }
    }
}