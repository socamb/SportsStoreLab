using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStoreAutomation;

namespace SportsStore.Tests.Selenium
{
    /// <summary>
    /// Summary description for SmokeTests
    /// </summary>

    [TestClass]
    public class LoginTests
    {
        [TestInitialize]
        public void Init()
        {
            Driver.Initalize();
        }


        [TestMethod]
        [TestCategory("CI_Build")]
        // Goal is to make the unit test easy to develop and self documenting.
        public void Selenium_Admin_User_Can_Login()
        {
            LoginPage.GoTo();
            LoginPage.LoginAs("Admin").WithPassword("TopSecret").Login();
            Assert.AreEqual("All Products", MaintenancePage.AllProducts, "Failed to Login");
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                Driver.Close();
            }
            catch
            {
                return;
            }
        }
    }
}
