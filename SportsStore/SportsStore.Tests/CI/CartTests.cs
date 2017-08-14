using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Moq;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests.CI
{
    [TestClass]
    public class CartTests
    {

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Add_New_Lines()
        {

            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity,11);
            Assert.AreEqual(results[1].Quantity, 1);

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // Arrange - create a new cart
            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
        }


        // These tests start using a mock product repository 
        // 
        [TestMethod]
        [TestCategory("CI_Build")]
        public void Calculate_Cart_Total()
        {
            
            // Arrange - create some products with prices
            Product p1 = new Product { ProductID = 1, Name = "P1", Price= 100m };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50m };

            // Arrange - create a new cart
            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            // Assert
            Assert.AreEqual(450m, result);

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100m };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50m };

            // Arrange - create a new cart
            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // act
            target.Clear();

            // assert
            Assert.AreEqual(target.Lines.Count(), 0);

        }


        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_Add_To_Cart()
        {
            // Arrange - create a mox repository for products
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="p1", Category="Apples" }
            }.AsQueryable);
       
            // Arrange - create a new cart
            Cart cart = new Cart();
            CartController target = new CartController(mock.Object,null);

            // Act - add product to cart
            target.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.Name, "p1");

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange - create a mox repository for products
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Name="p1", Category="Apples" }
            }.AsQueryable);

            // Arrange - create a new cart
            Cart cart = new Cart();
            CartController target = new CartController(mock.Object, null);

            // Act - add product to cart
            RedirectToRouteResult result = target.AddToCart(cart, 1, "MyUrl");

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyUrl", result.RouteValues["returnUrl"]);
        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Can_View_Cart_Contents()
        {
            // Arrange - create cart
            Cart cart = new Cart();
            CartController target = new CartController(null, null);

            // Act - call the index action method
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "MyUrl").ViewData.Model;

            // Assert 
            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("MyUrl", result.ReturnUrl);

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController target = new CartController(null, mock.Object);

            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            //assert - check that the order hasent been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            //assert default viewing is returned
            Assert.AreEqual(result.ViewName, "");

            // assert - chack that I am passing an invalid model to the view
            Assert.AreEqual(result.ViewData.ModelState.IsValid, false);

        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController target = new CartController(null, mock.Object);
            target.ModelState.AddModelError("error", "error");

            // Act
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //assert - check that the order hasent been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            //assert default viewing is returned
            Assert.AreEqual(result.ViewName, "");

            // assert - check that I am passing an invalid model to the view
            Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
        }

        [TestMethod]
        [TestCategory("CI_Build")]
        public void Cannot_Checkout_And_Submit_Order()
        {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController target = new CartController(null, mock.Object);

            // Act
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //assert - check that the order hasent been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

            //assert viewing is returned Completed View
            Assert.AreEqual("Completed", result.ViewName);

            // assert - check that I am passing an valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }


        }

}
