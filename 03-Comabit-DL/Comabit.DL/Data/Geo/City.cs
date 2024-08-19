// <copyright file="City.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Geo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Ort, 4. Admin Ebene
    /// </summary>
    public class City
    {
        [Key]
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

        public int Population
        {
            get;
            set;
        }

        public GeoPlaceAccuracy GeoPositionAccuracy
        {
            get;
            set;
        }

        public Guid CommunityId
        {
            get;
            set;
        }

        public Community Community
        {
            get;
            set;
        }
    }
}