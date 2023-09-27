using Microsoft.AspNetCore.DataProtection.Repositories;
using mvc_in_memory_data_store.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace mvc_in_memory_data_store.Test
{
    [NonParallelizable]
    [TestFixture]
    public class CoreTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Order(1)]
        [NonParallelizable]
        [Test]
        public void SampleTest()
        {   
        }
    }
}
