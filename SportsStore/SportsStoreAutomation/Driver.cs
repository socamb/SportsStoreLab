using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace SportsStoreAutomation
{

    // There will be a driver for each browser type. The default uses FireFox
    public class Driver
    {
        public static IWebDriver Instance { get; set; } 

        public static void Initalize()
        {
            Instance = new FirefoxDriver();

            // This is where Selinium Server would be setup. See if this works first then come up with 
            // A good implementation

            //Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"),
            //                               DesiredCapabilities.Firefox());

            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        public static void Close()
        {
            Instance.Close();
        }
    }
}
