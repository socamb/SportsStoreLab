using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStoreAutomation;

namespace SportsStore.Tests.Selenium
{

    // This base class is used by all of our Selenium tests (a)
    public class SeleniumBase
    {

        // We need this object in order to get values for the test.runsettings file
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        private TestContext testContextInstance;
        public string Url;


        // Initalize the driver and set the URL we will use for our Selenium Tests
        [TestInitialize]
        public void Init()
        {
            Url = TestContext.Properties["appUrl"].ToString();
            Driver.Initalize();
        }


        // Close the driver and handle runtime errors
        [TestCleanup]
        public void Cleanup()
        {
            try
            { Driver.Close(); }
            catch
            { return; }
        }
    }
}
