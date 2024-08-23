// <copyright file="SellerDoc.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Data.ElasticSearch
{
    using System;
    using System.Collections.Generic;

    public class SellerDoc
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

        public ICollection<Guid> CategoryIds
        {
            get;
            set;
        }

        public ICollection<CategoryDoc> Categories
        {
            get;
            set;
        }

        public ICollection<Guid> SubCategoryIds
        {
            get;
            set;
        }

        public ICollection<CategoryDoc> SubCategories
        {
            get;
            set;
        }

        public ICollection<CityDoc> Cities 
        {
            get; set;
        }

        public SellerDoc()
        {
            this.Categories = new HashSet<CategoryDoc>();
            this.SubCategories = new HashSet<CategoryDoc>();
            this.Cities = new HashSet<CityDoc>();
            this.CategoryIds = new HashSet<Guid>();
            this.SubCategoryIds = new HashSet<Guid>();
        }
    }
}