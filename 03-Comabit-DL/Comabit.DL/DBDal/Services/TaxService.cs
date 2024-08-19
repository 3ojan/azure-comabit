// <copyright file="TaxService.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Services
{
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class TaxService : ITaxService
    {
        //private readonly IConfiguration Configuration;
        private readonly string BaseUrl;
        private readonly string RequestingTaxId;

        public TaxService() //IConfiguration configuration)
        {
            //Configuration = configuration;
            BaseUrl = "https://evatr.bff-online.de/evatrRPC";
            RequestingTaxId = "DE207276787"; // Configuration["TaxService:RequestingTaxId"];
        }

        public async Task<Dictionary<string, string>> CheckIdNumber(string taxIdNumber)
        {
            using (var webClient = new WebClient())
            {
                var url = $"{BaseUrl}?UstId_1={RequestingTaxId}&UstId_2={taxIdNumber}";
                var response = await webClient.DownloadStringTaskAsync(url);

                return ParseResponse(response);
            }
        }

        public async Task<Dictionary<string, string>> CheckIdNumberQualified(string taxIdNumber, string companyName, string city, string postalCode = "", string street = "")
        {
            using (var webClient = new WebClient())
            {
                var url = $"{BaseUrl}?UstId_1={RequestingTaxId}&UstId_2={taxIdNumber}&Firmenname={companyName}&Ort={city}&PLZ={postalCode}&Strasse={street}";
                var response = await webClient.DownloadStringTaskAsync(url);

                return ParseResponse(response);
            }
        }

        private static Dictionary<string, string> ParseResponse(string response)
        {
            XDocument doc = XDocument.Parse(response);

            /*
            Ergebnis ist solch eine Struktur:

            <params>
            <param>
            <value><array><data>
            <value><string>UstId_1</string></value>
            <value><string>DE207276787</string></value>
            </data></array></value>
            </param>
            <param>
            <value><array><data>
            <value><string>ErrorCode</string></value>
            <value><string>200</string></value>
            </data></array></value>
            </param>
            */

            // aus dieser flachen Struktur ein Dictionary machen:

            var data = doc.Descendants("params").FirstOrDefault().Descendants("param").Select(x => new
            {
                Key = x.Descendants("data").Descendants("string").FirstOrDefault().Value,
                Value = x.Descendants("data").Descendants("string").Skip(1).FirstOrDefault().Value
            }).ToDictionary(d => d.Key, d => d.Value);

            return data;
        }
    }
}