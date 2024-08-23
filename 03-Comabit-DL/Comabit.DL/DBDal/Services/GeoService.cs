// <copyright file="GeoService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GeoService : IGeoService
    {
        private IUnitOfWork unitOfWork;

        private readonly IGenericRepository<State> _stateRepository;
        private readonly IGenericRepository<Province> _provinceRepository;
        private readonly IGenericRepository<Community> _communityRepository;
        private readonly IGenericRepository<City> _cityRepository;

        public GeoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this._stateRepository = new GenericRepository<State>(this.unitOfWork.DbContext);
            this._provinceRepository = new GenericRepository<Province>(this.unitOfWork.DbContext);
            this._communityRepository = new GenericRepository<Community>(this.unitOfWork.DbContext);
            this._cityRepository = new GenericRepository<City>(this.unitOfWork.DbContext);
        }

        public IQueryable<State> GetAllStates()
        {
            return this._stateRepository.GetAll();
        }

        public IQueryable<Province> GetAllProvinces()
        {
            return this._provinceRepository.GetAll();
        }

        public IQueryable<Community> GetAllCommunities()
        {
            return this._communityRepository.GetAll();
        }

        public IQueryable<City> GetAllCities()
        {
            return this._cityRepository.GetAll();
        }

        public IQueryable<Community> GetCommunitiesForState(Guid stateId)
        {
            return this._communityRepository.GetAll().Where(c => c.Province.StateId == stateId);
        }

        public IQueryable<Community> GetCommunitiesByIds(ICollection<Guid> communityIds)
        {
            return this._communityRepository.GetAll().Where(c => communityIds.Contains(c.Id));
        }

        public IQueryable<Community> GetCommunity(Guid id)
        {
            return this._communityRepository.GetAll().Where(c => c.Id == id);
        }

        public IQueryable<City> GetCitiesForCommunity(Guid communityId)
        {
            return this._cityRepository.GetAll().Where(c => c.CommunityId == communityId);
        }

        public IQueryable<City> GetCityByPostalCode(string postalCode)
        {
            return this._cityRepository.GetAll().Where(c => c.PostalCode == postalCode);
        }

        public IQueryable<State> SearchState(string q)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Community> SearchCommunity(string q)
        {
            throw new NotImplementedException();
        }

        public IQueryable<City> SearchCity(string q)
        {
            throw new NotImplementedException();
        }

        public void Add(State state)
        {
            this._stateRepository.Add(state);
        }

        public void Add(Province province)
        {
            this._provinceRepository.Add(province);
        }

        public void Add(Community community)
        {
            this._communityRepository.Add(community);
        }

        public void Add(City city)
        {
            this._cityRepository.Add(city);
        }

        public void Update(State state)
        {
            this._stateRepository.Update(state);
        }

        public void Update(Province province)
        {
            this._provinceRepository.Update(province);
        }

        public void Update(Community community)
        {
            this._communityRepository.Update(community);
        }

        public void Update(City city)
        {
            this._cityRepository.Update(city);
        }

        public void Delete(State state)
        {
            this._stateRepository.Delete(state);
        }

        public void Delete(Province province)
        {
            this._provinceRepository.Delete(province);
        }

        public void Delete(Community community)
        {
            this._communityRepository.Delete(community);
        }

        public void Delete(City city)
        {
            this._cityRepository.Delete(city);
        }

        public async Task<int> SaveAsync()
        {
            return await this.unitOfWork.SaveAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();
        }
    }
}