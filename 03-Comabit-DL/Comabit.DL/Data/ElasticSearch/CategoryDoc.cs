// <copyright file="CategoryDoc.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.ElasticSearch
{
    using System;

    public class CategoryDoc
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Tags 
        { 
            get; 
            set; 
        }
    }
}