// <copyright file="HomeController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Controllers
{
    using Comabit.BL.Geo;
    using Comabit.DL;
    using Comabit.DL.Interfaces;
    using Comabit.Helpers;
    using Comabit.UI.Areas.Authentication.Models;
    using Comabit.UI.Areas.Authentication.Models.Geo;
    using Comabit.UI.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GeoController : BaseController
    {
        private readonly GeoManager _geoManager;
        

        public GeoController(GeoManager geoManager, ILogger<GeoController> logger) : base(logger)
        {
            this._geoManager = geoManager;
        }

        [AllowAnonymous]
        public async Task<JsonNetResult> GetCommunitiesForState(Guid id)
        {
            ICollection<CommunityViewModel> communities = this.Mapper.Map<ICollection<CommunityViewModel>>(await this._geoManager.GetCommunitiesForState(id));

            return new JsonNetResult(new
            {
                status = "ok",
                html = await this.RenderViewAsync("_SellerGeoSelection", communities),
            });
        }
    }
}