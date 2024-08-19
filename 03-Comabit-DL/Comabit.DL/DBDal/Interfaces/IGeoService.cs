// <copyright file="IGeoService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using Comabit.DL.Data.Geo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGeoService : IAsyncDisposable
    {
        IQueryable<State> GetAllStates();

        IQueryable<Province> GetAllProvinces();

        IQueryable<Community> GetAllCommunities();

        IQueryable<City> GetAllCities();

        IQueryable<Community> GetCommunitiesForState(Guid stateId);

        IQueryable<Community> GetCommunitiesByIds(ICollection<Guid> communityIds);

        IQueryable<Community> GetCommunity(Guid id);

        IQueryable<City> GetCitiesForCommunity(Guid communityId);

        IQueryable<City> GetCityByPostalCode(string postalCode);

        IQueryable<State> SearchState(string q);

        IQueryable<Community> SearchCommunity(string q);

        IQueryable<City> SearchCity(string q);

        void Add(State state);

        void Add(Province province);

        void Add(Community community);

        void Add(City city);

        void Update(State state);

        void Update(Province province);

        void Update(Community community);

        void Update(City city);

        void Delete(State state);

        void Delete(Province province);

        void Delete(Community community);

        void Delete(City city);

        Task<int> SaveAsync();
    }
}