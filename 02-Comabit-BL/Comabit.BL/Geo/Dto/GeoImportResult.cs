// <copyright file="GeoNamesImportResult.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Geo.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Serializable]
    public class GeoImportResult
    {

        public string Message
        {
            get;
            set;
        }

        public GeoImportResult(string message)
        {
            Message = message;
        }
    }
}