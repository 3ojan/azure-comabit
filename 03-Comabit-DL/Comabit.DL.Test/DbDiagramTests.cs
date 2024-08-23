// <copyright file="CompanyServiceTests.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

using Comabit.DL.Data.Company;
using Comabit.DL.Data.Portfolio;
using Comabit.DL.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comabit.DL.Test
{
    public class DBDiagramTestes : BaseServiceTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllCompanies()
        {
            var path = Path.GetTempFileName() + ".dgml";
            File.WriteAllText(path, this.UnitOfWork.DbContext.AsDgml(), Encoding.UTF8);

            var startInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true,
            };
            Process.Start(startInfo);
        }
    }
}