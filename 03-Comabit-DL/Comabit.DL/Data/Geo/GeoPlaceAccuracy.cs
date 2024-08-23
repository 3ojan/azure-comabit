// <copyright file="GeoPlaceAccuracy.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Geo
{
    using System.ComponentModel;

    public enum GeoPlaceAccuracy
    {
        [Description("estimated")]
        Estimated = 1,

        [Description("geonameid")]
        Geonameid = 4,

        [Description("centroid of addresses or shape")]
        CentroidOfAddressesOrShap = 6,
    }
}