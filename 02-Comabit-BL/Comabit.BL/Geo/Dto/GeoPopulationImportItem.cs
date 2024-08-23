// <copyright file="GeoPopulationImportItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Geo.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Serializable]
    public class GeoPopulationImportItem
    {

        public int Population
        {
            get;
            set;
        }

        public string Zipcode
        {
            get;
            set;
        }
    }
}