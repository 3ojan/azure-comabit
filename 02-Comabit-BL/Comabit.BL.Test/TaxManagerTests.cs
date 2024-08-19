// <copyright file="TaxManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Geo;
using Comabit.BL.Porfolio;
using Comabit.BL.Tax.Dto;
using Comabit.DL.Interfaces;
using Comabit.DL.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.BL.Test
{
    public class TaxManagerTests
    {
        private TaxManager _taxManager;
        private ITaxService _taxService;

        [SetUp]
        public void Setup()
        {
            this._taxService = new TaxService();
            this._taxManager = new TaxManager(this._taxService);
        }

        [Test]
        public async ValueTask TestExistingTaxId()
        {
            var response = await this._taxManager.CheckTaxId("ATU69706035");

            Assert.That(response.State.Type == TaxIdCheckStateType.Valid);
        }

        [Test]
        public async ValueTask TestNonExistingTaxId()
        {
            var response = await this._taxManager.CheckTaxId("XXXXXXXX");

            Assert.That(response.State.Type == TaxIdCheckStateType.Invalid);
        }

        [Test]
        public async ValueTask TestExistingTaxIdAndCorrectFieldsQualified()
        {
            var response = await this._taxManager.CheckTaxIdQualified("ATU69706035", "Kneipp Austria GmbH", "Wiener Neudorf");

            Assert.That(response.State.Type == TaxIdCheckStateType.Valid && !response.InvalidFields.Any());
        }

        [Test]
        public async ValueTask TestExistingTaxIdWithFalseCompanyQualified()
        {
            var response = await this._taxManager.CheckTaxIdQualified("ATU69706035", "Kneipp Germany", "Anderer Ort");

            Assert.That(response.State.Type == TaxIdCheckStateType.Valid 
                && response.InvalidFields.Any(f => f == TaxIdCheckFieldType.CompanyName)
                && response.InvalidFields.Any(f => f == TaxIdCheckFieldType.City)
                && response.InvalidFields.Count() == 2);
        }
    }
}