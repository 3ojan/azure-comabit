using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Geo.Dto
{
    [Serializable]
    public class ProvinceItem
    {
        public Guid Id
        {
            get;
            set;
        }

        public string AgsCode
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<CommunityItem> Communities
        {
            get;
            set;
        }
    }
}