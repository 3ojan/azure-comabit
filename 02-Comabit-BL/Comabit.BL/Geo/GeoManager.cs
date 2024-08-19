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

    public class GeoManager : BaseManager
    {
        private IGeoService _geoService;

        public GeoManager(IGeoService geoService)
        {
            this._geoService = geoService;
        }

        public async Task<IEnumerable<StateItem>> GetStates()
        {
            var states = await this._geoService.GetAllStates().OrderBy(g => g.Name).ToListAsync();

            return states.Select(e => this.Mapper.Map<State, StateItem>(e)).ToList();
        }

        public async Task<ICollection<CommunityItem>> GetNotAssignedCommunities()
        {
            ICollection<CommunityItem> communityItems = this.Mapper.Map<ICollection<CommunityItem>>(await this._geoService.GetAllCommunities().Include(g => g.SellerCompanies).Where(c => !c.SellerCompanies.Any()).ToListAsync());

            return communityItems;
        }

        public async Task<IEnumerable<CommunityItem>> GetCommunitiesForState(Guid stateId)
        {
            if (stateId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(stateId));
            }

            var communities = await this._geoService.GetCommunitiesForState(stateId).OrderBy(g => g.Name).ToListAsync();

            return communities.Select(e => this.Mapper.Map<Community, CommunityItem>(e)).ToList();
        }

        public async Task<IEnumerable<CityItem>> GetCitiesForCommunity(Guid communityId)
        {
            if (communityId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(communityId));
            }

            var cities = await this ._geoService.GetCitiesForCommunity(communityId).OrderBy(g => g.Name).ToListAsync();

            return cities.Select(e => this.Mapper.Map<City, CityItem>(e)).ToList();
        }

        public async Task<CityItem> GetCityByPostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                throw new ArgumentNullException(nameof(postalCode));
            }

            var city = await this._geoService.GetCityByPostalCode(postalCode).FirstOrDefaultAsync();

            return this.Mapper.Map<City, CityItem>(city);
        }

        public void AddState(StateItem stateModel)
        {
            var entity = this.Mapper.Map<StateItem, State>(stateModel);

            this._geoService.Add(entity);
        }

        public void AddProvince(ProvinceItem provinceModel)
        {
            var entity = this.Mapper.Map<ProvinceItem, Province>(provinceModel);

            this._geoService.Add(entity);
        }

        public void AddCommunity(CommunityItem communityModel)
        {
            var entity = this.Mapper.Map<CommunityItem, Community>(communityModel);

            this._geoService.Add(entity);
        }

        public void AddCity(CityItem cityModel)
        {
            var entity = this.Mapper.Map<CityItem, City>(cityModel);

            this._geoService.Add(entity);
        }

        public async Task SaveAsync()
        {
            await this._geoService.SaveAsync();
        }

        public async Task<CommunityItem> GetCommunity(Guid id)
        {
            return this.Mapper.Map<CommunityItem>(await this._geoService.GetCommunity(id).FirstOrDefaultAsync());
        }
    }
}