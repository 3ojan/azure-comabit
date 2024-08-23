// <copyright file="ConfigFactory.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit
{
    using Microsoft.Extensions.Configuration;
    using System;

    public class ConfigFactory
    {
        public static Config CreateConfig()
        {
            var result = new Config()
            {
                Level = RetrieveLevel(),
                ConnectionString = RetrieveConnectionString(),
                LicenseKey = RetrieveLicenseKey(),
            };

            return result;
        }

        private static Level RetrieveLevel()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            var level = configuration.GetSection("Level").Value;


            if (string.IsNullOrEmpty(level))
            {
                throw new InvalidOperationException("Config: Level wurde nicht gefunden");
            }

            var result = Level.None;
            if (level.Equals("Test"))
            {
                result = Level.Test;
            }
            if (level.Equals("Prod"))
            {
                result = Level.Prod;
            }

            return result;
        }

        private static string RetrieveConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            var result = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(result))
            {
                throw new InvalidOperationException("Config: ConnectionString wurde nicht gefunden");
            }

            return result;
        }

        private static string RetrieveLicenseKey()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            var result = configuration.GetSection("LicenseKey").Value;

            if (string.IsNullOrEmpty(result))
            {
                throw new InvalidOperationException("Config: LicenseKey wurde nicht gefunden");
            }

            return result;
        }
    }
}
