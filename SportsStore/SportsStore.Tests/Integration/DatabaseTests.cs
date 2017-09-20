using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Tests.Integration
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        [TestCategory("Nightly_Build")]
        public void DBAddTests()
        {
            Assert.AreEqual(1, 1);
        }


        // This is run by the Nightly release to the Build Environment, not the CI Build
        [TestMethod]
        [TestCategory("Nightly_Build")]
        public void Can_AddProductToDb()
        {
            IProductRepository myProductRepo = new EFProductRepository();
            Product myProduct = new Product { Name = "xxx", Category = "123", Description = "desc", Price = 20m };

            myProductRepo.SaveProduct(myProduct);
        }



    }
}
