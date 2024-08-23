// <copyright file="GeoManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Geo
{
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Shared;
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Comabit.BL.Tax;
    using Comabit.BL.Tax.Dto;

    public class TaxManager : BaseManager
    {
        private ITaxService _taxService;

        public TaxManager(ITaxService taxService)
        {
            this._taxService = taxService;
        }

        public async Task<TaxIdCheckResponse> CheckTaxId(string taxId)
        {
            var responseData = await this._taxService.CheckIdNumber(taxId);

            TaxIdCheckResponse response = new TaxIdCheckResponse(responseData);

            return response;
        }

        public async Task<TaxIdCheckResponse> CheckTaxIdQualified(string taxId, string companyName, string city, string postalCode = "", string street = "")
        {
            var responseData = await this._taxService.CheckIdNumberQualified(taxId, companyName, city, postalCode, street);

            TaxIdCheckResponse response = new TaxIdCheckResponse(responseData);

            return response;
        }
    }
}