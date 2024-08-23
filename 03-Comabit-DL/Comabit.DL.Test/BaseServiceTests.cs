// <copyright file="BaseServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.DL.Test
{
    public class BaseServiceTests
    {
        private IUnitOfWork _unitOfWork;

        public BaseServiceTests()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseNpgsql(connectionString)
                                .Options;

            var dbContext = new ApplicationDbContext(contextOptions);
            this._unitOfWork = new UnitOfWork(dbContext);
        }

        public IUnitOfWork UnitOfWork => this._unitOfWork;
    }
}