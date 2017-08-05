
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
            Assert.AreEqual("All Products", MaintenancePage.AllProducts, "Failed to Login");
        }



        
    }
}
