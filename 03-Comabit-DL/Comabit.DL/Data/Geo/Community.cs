// <copyright file="Community.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Geo
{
    using Comabit.DL.Data.Company;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Landkreis, 3. Admin Ebene
    /// </summary>
    public class Community
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

        public ICollection<City> Cities
        {
            get;
            set;
        }

        public Guid ProvinceId
        {
            get;
            set;
        }

        public Province Province
        {
            get;
            set;
        }

        public ICollection<Seller> SellerCompanies { get; set; }

        public Community()
        {
            this.Cities = new HashSet<City>();
            this.SellerCompanies = new HashSet<Seller>();
        }
    }
}