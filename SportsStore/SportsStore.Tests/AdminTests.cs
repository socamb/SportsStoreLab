using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Moq;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using System.Collections.Generic;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        [TestCategory("CI_Build")]
        public void Index_Contains_All_Products()
        {
            // Arrange - create a mock repository for products Changed by Sue
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" },
            });

            AdminController target = new AdminController(mock.Object);

            // Action
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            //Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Edit_Product()
        {
            // Arrange - create a mox repository for products
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" },
            });

            AdminController target = new AdminController(mock.Object);

            //act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
          }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Cannot_Edit_Nonexistant_Product()
        {
            // Arrange - create a mox repository for products
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=2, Name="P2" },
                new Product {ProductID=3, Name="P3" },
            });

            AdminController target = new AdminController(mock.Object);

            //act
            Product result = target.Edit(4).ViewData.Model as Product;


            // assert

            Assert.IsNull(result);

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create a mox repository for products
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "test" };

            // act - try to save
            ActionResult result = target.Edit(product);

            // assert - check that the repository was called
            mock.Verify(M => M.SaveProduct(product));
            // check the type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));


        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create a mox repository for products
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "test" };
            target.ModelState.AddModelError("error", "error");

            // act - try to save
            ActionResult result = target.Edit(product);

            // assert - check that the repository was not called
            mock.Verify(M => M.SaveProduct(product), Times.Never());
            // check the type
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Delete_Valid_Products()
        {
            //Arrange
            Product prod = new Product { ProductID = 2, Name = "Test" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="P1" },
                new Product {ProductID=3, Name="P3" },
            });
            AdminController target = new AdminController(mock.Object);

            //act delete the product
            target.Delete(prod.ProductID);

            //assert
            mock.Verify(m => m.DeleteProduct(prod.ProductID));

        }

    }
}
