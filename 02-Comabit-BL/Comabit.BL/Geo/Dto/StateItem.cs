using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Geo.Dto
{
    [Serializable]
    public class StateItem
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

        public ICollection<ProvinceItem> Provinces
        {
            get;
            set;
        }
    }
}