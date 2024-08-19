// <copyright file="GeoNamesImportItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Geo.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Serializable]
    public class GeoNamesImportItem
    {

        public string CountryCode
        {
            get;
            set;
        }

        public string Zipcode
        {
            get;
            set;
        }

        public string Place
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string StateCode
        {
            get;
            set;
        }

        public string Province
        {
            get;
            set;
        }

        public string ProvinceCode
        {
            get;
            set;
        }

        public string Community
        {
            get;
            set;
        }

        public string CommunityCode
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

        public int Population
        {
            get;
            set;
        }

        public string StateAgsCodeFromCommunityCode => !string.IsNullOrEmpty(CommunityCode) ? CommunityCode.Substring(0, 2) : StateCode;

        internal StateItem GetStateItem()
        {
            return new StateItem()
            {
                Id = System.Guid.NewGuid(),
                AgsCode = StateAgsCodeFromCommunityCode,
                Name = State
            };
        }

        internal ProvinceItem GetProvinceItem()
        {
            return new ProvinceItem()
            {
                Id = System.Guid.NewGuid(),
                AgsCode = ProvinceCode,
                Name = Province
            };
        }

        internal CommunityItem GetCommunityItem()
        {
            return new CommunityItem()
            {
                Id = System.Guid.NewGuid(),
                AgsCode = CommunityCode,
                Name = Community
            };
        }

        internal CityItem GetCityItem()
        {
            return new CityItem()
            {
                Id = System.Guid.NewGuid(),
                Name = Place,
                AgsCode = "",
                Latitude = Latitude,
                Longitude = Longitude,
                Population = Population,
                PostalCode = Zipcode                
            };
        }
    }
}