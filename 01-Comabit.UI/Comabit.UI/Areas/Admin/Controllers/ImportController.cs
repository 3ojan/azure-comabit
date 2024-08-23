// <copyright file="ImportController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Admin.Controllers
{
    using Comabit.BL.Geo;
    using Comabit.BL.Geo.Dto;
    using Comabit.UI.Controllers;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class ImportController : BaseController
    {
        GeoImportManager GeoImportManager
        {
            get; 
            set;
        }

        public ImportController(ILogger<CompanyController> logger, GeoImportManager geoImportManager) : base(logger)
        {
            GeoImportManager = geoImportManager;
        }

        public async Task<ActionResult> ImportGeoNamesItems()
        {
            GeoImportResult importResult = await GeoImportManager.ImportGeoNamesItems();

            return Json(importResult);
        }

        public async Task<ActionResult> UpdateGeoPopulation()
        {
            GeoImportResult importResult = await GeoImportManager.UpdateGeoPopulation();

            return Json(importResult);
        }
    }
}