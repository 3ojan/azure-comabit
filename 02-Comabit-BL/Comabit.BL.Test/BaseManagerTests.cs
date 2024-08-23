// <copyright file="BaseManagerTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.BL.Shared;
using Comabit.DL;
using Comabit.DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.BL.Test
{
    public class BaseManagerTests : AutoMapperManager
    {
        private IUnitOfWork _unitOfWork;

        public BaseManagerTests()
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