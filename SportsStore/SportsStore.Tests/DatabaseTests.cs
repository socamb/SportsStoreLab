using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Tests
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


        [TestMethod]
        [TestCategory("Nightly_Build")]
        public void Can_ABC()
        {
            IProductRepository myProductRepo = new EFProductRepository();
            Product myProduct = new Product { Name = "xxx", Category = "123", Description = "desc", Price = 20m };

            myProductRepo.SaveProduct(myProduct);








        }



    }
}
