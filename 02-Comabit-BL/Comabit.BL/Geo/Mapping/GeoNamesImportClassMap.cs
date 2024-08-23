using Comabit.BL.Geo.Dto;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Geo.Mapping
{
    public sealed class GeoNamesImportClassMap : ClassMap<GeoNamesImportItem>
    {
        public GeoNamesImportClassMap()
        {
            Map(m => m.CountryCode).Name("country_code");
            Map(m => m.Zipcode).Name("zipcode");
            Map(m => m.Place).Name("place");
            Map(m => m.State).Name("state");
            Map(m => m.StateCode).Name("state_code");
            Map(m => m.Province).Name("province");
            Map(m => m.ProvinceCode).Name("province_code");
            Map(m => m.Community).Name("community");
            Map(m => m.CommunityCode).Name("community_code");
            Map(m => m.Latitude).Name("latitude");
            Map(m => m.Longitude).Name("longitude");
        }
    }
}
