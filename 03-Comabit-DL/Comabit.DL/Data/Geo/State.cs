// <copyright file="State.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.Geo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Bundesland, 1. Admin Ebene
    /// </summary>
    public class State
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

        public ICollection<Province> Provinces
        {
            get;
            set;
        }

        public State()
        {
            this.Provinces = new HashSet<Province>();
        }
    }
}