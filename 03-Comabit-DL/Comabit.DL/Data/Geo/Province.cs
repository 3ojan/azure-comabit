// <copyright file="Province.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Geo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Regierungsbezirk, 2. Admin Ebene
    /// </summary>
    public class Province
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

        public ICollection<Community> Communities
        {
            get;
            set;
        }

        public Guid StateId
        {
            get;
            set;
        }

        public State State
        {
            get;
            set;
        }

        public Province()
        {
            this.Communities = new HashSet<Community>();
        }
    }
}