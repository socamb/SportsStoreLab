
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStoreAutomation;

namespace SportsStore.Tests.Selenium
{
    /// <summary>
    /// Summary description for SmokeTests
    /// </summary>

    [TestClass]
    public class LoginTests : SeleniumBase
    {
        [TestMethod]
        [TestCategory("Nightly_Build")]
        // Goal is to make the unit test easy to develop and self documenting.
        public void Selenium_Admin_User_Can_Login()
        {
            LoginPage.GoTo(Url);
            LoginPage.LoginAs("Admin").WithPassword("TopSecret").Login();
            Assert.AreEqual("All Products", MaintenancePage.ReturnPageTitle, "Failed to Login");
        }

        [TestMethod]
        [TestCategory("Nightly_Build")]
        public void Selenium_Can_Add_Product()
        {
            LoginPage.GoTo(Url);
            LoginPage.LoginAs("Admin").WithPassword("TopSecret").Login();
            MaintenancePage.AddNewProduct("SelNamez", "SelDescription", 2, "Selenium");
            MaintenancePage.SelectProduct("SelName");
            Assert.AreEqual("Edit SelName", MaintenancePage.ReturnPageTitle, "Invalid Product");

            //MaintenancePage.ValidateProduct("SelName", "SelDescription", 2, "Selenium");


            //MaintenencePage.DeleteCategory(Category)
        }


        
    }
}
