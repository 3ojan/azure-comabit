using Comabit.BL.Geo.Dto;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Geo.Mapping
{
    public sealed class GeoPopulationImportClassMap : ClassMap<GeoPopulationImportItem>
    {
        public GeoPopulationImportClassMap()
        {
            Map(m => m.Zipcode).Name("plz");
            Map(m => m.Population).Name("einwohner");
        }
    }
}
