using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Geo.Dto
{
    [Serializable]
    public class CityItem
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

        public string CommunityName
        {
            get;
            set;
        }

        public string PostalCode
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public int Population
        {
            get;
            set;
        }
    }
}