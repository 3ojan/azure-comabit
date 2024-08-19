// <copyright file="GeoImportManager.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Geo
{
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Geo.Mapping;
    using Comabit.BL.Shared;
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Interfaces;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    public class GeoImportManager : BaseManager
    {
        private IGeoService _geoService;

        private string RootDirectoryPath { get; set; }

        public GeoImportManager(IGeoService geoService)
        {
            this._geoService = geoService;
            RootDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Importiert Geo-Daten, die auf http://download.geonames.org/export/zip/ basieren
        /// </summary>
        /// <returns></returns>
        public async Task<GeoImportResult> ImportGeoNamesItems()
        {
            var uri = new Uri("https://raw.githubusercontent.com/zauberware/postal-codes-json-xml-csv/master/data/DE/zipcodes.de.csv");
            var downloadedFile = new FileInfo(Path.Combine(RootDirectoryPath, "App_Data/tmp/", System.Guid.NewGuid().ToString("N")));

            if (!downloadedFile.Directory.Exists)
            {
                downloadedFile.Directory.Create();
            }

            using var webClient = new WebClient();
            await webClient.DownloadFileTaskAsync(uri, downloadedFile.FullName);

            List<GeoNamesImportItem> records = GetGeoNamesRecordsFromFile(downloadedFile.FullName);

            if (downloadedFile.Exists)
            {
                downloadedFile.Delete();
            }

            List<State> existingStateRecords = await this._geoService.GetAllStates().ToListAsync();
            List<Province> existingProvinceRecords = await this._geoService.GetAllProvinces().ToListAsync();
            List<Community> existingCommunityRecords = await this._geoService.GetAllCommunities().ToListAsync();
            List<City> existingCityRecords = await this._geoService.GetAllCities().ToListAsync();

            foreach (var record in records)
            {
                var currentState = existingStateRecords.Where(s => s.AgsCode == record.StateAgsCodeFromCommunityCode).FirstOrDefault();

                if (currentState == null)
                {
                    var newStateItem = record.GetStateItem();
                    currentState = this.Mapper.Map<StateItem, State>(newStateItem);

                    this._geoService.Add(currentState);
                    existingStateRecords.Add(currentState);
                }

                var currentProvince = existingProvinceRecords.Where(p => p.AgsCode == record.ProvinceCode && p.StateId.Equals(currentState.Id)).FirstOrDefault();

                if (currentProvince == null)
                {
                    var newProvinceItem = record.GetProvinceItem();
                    currentProvince = this.Mapper.Map<ProvinceItem, Province>(newProvinceItem);
                    currentProvince.StateId = currentState.Id;

                    this._geoService.Add(currentProvince);
                    existingProvinceRecords.Add(currentProvince);
                }

                var currentCommunity = existingCommunityRecords.Where(c => c.AgsCode == record.CommunityCode && c.ProvinceId.Equals(currentProvince.Id)).FirstOrDefault();

                if (currentCommunity == null)
                {
                    var newCommunityItem = record.GetCommunityItem();
                    currentCommunity = this.Mapper.Map<CommunityItem, Community>(newCommunityItem);
                    currentCommunity.ProvinceId = currentProvince.Id;

                    this._geoService.Add(currentCommunity);
                    existingCommunityRecords.Add(currentCommunity);
                }

                var currentCity = existingCityRecords.Where(c => c.PostalCode == record.Zipcode && c.Name == record.Place && c.CommunityId.Equals(currentCommunity.Id)).FirstOrDefault();

                if (currentCity == null)
                {
                    var newCityItem = record.GetCityItem();
                    currentCity = this.Mapper.Map<CityItem, City>(newCityItem);
                    currentCity.CommunityId = currentCommunity.Id;

                    this._geoService.Add(currentCity);
                    existingCityRecords.Add(currentCity);
                }
            }

            int changes = await SaveAsync();

            if (records.Any())
            {
                return new GeoImportResult($"Import erfolgreich: {changes} Änderungen durchgeführt.");
            }
            else 
            { 
                return new GeoImportResult($"Keine Objekte für den Import gefunden."); 
            }
        }

        private List<GeoNamesImportItem> GetGeoNamesRecordsFromFile(string file)
        {
            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("en"))
            {
                MissingFieldFound = null,
                Delimiter = ",",
                Quote = '"',
                TrimOptions = TrimOptions.Trim
            };

            csvConfig.RegisterClassMap<GeoNamesImportClassMap>();

            List<GeoNamesImportItem> records = new List<GeoNamesImportItem>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                records = csv.GetRecords<GeoNamesImportItem>().ToList();
            }

            return records;
        }

        /// <summary>
        /// Importiert Einwohner-Zahlen auf bestehende City-Entities anhand PLZ aus lokaler CSV-Datei,
        /// Quelle plz_einwohner.csv und zuordnung_plz_ort_landkreis: https://www.suche-postleitzahl.org/downloads
        /// </summary>
        /// <returns></returns>
        public async Task<GeoImportResult> UpdateGeoPopulation()
        {
            var dir = AppDomain.CurrentDomain.GetData("DataDirectory");

            var importFile = new FileInfo(Path.Combine(dir.ToString(), "geo/plz_einwohner.csv"));

            if (!importFile.Exists)
            {
                return new GeoImportResult("Fehler: Einwohner CSV Datei nicht gefunden.");
            }

            List<GeoPopulationImportItem> records = GetGeoPopulationRecordsFromFile(importFile.FullName);

            List<City> existingCityRecords = await this._geoService.GetAllCities().ToListAsync();

            foreach (var record in records)
            {
                existingCityRecords.Where(c => c.PostalCode == record.Zipcode).ToList().ForEach(e => e.Population = record.Population); ;
            }

            int changes = await SaveAsync();

            if (records.Any())
            {
                return new GeoImportResult($"Import erfolgreich: {changes} Änderungen durchgeführt.");
            }
            else
            {
                return new GeoImportResult($"Keine Objekte für den Import gefunden.");
            }
        }

        private List<GeoPopulationImportItem> GetGeoPopulationRecordsFromFile(string file)
        {
            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("en"))
            {
                MissingFieldFound = null,
                Delimiter = ",",
                Quote = '"',
                TrimOptions = TrimOptions.Trim
            };

            csvConfig.RegisterClassMap<GeoPopulationImportClassMap>();

            List<GeoPopulationImportItem> records = new List<GeoPopulationImportItem>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                records = csv.GetRecords<GeoPopulationImportItem>().ToList();
            }

            return records;
        }

        public async Task<int> SaveAsync()
        {
            return await this._geoService.SaveAsync();
        }
    }
}