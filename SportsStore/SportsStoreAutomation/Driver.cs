using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SportsStoreAutomation
{

    // There will be a driver for each browser type. The default uses FireFox
    public class Driver
    {
        public static IWebDriver Instance { get; set; } 

        public static void Initalize()
        {
            Instance = new FirefoxDriver();
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        public static void Close()
        {
            Instance.Close();
        }
    }
}
