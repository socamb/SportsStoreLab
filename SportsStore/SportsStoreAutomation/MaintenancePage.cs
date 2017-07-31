using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SportsStoreAutomation
{
    public class MaintenancePage
    {

        public static string AllProducts
        {
            get
            {

                // Still expermenting how to do this best. For now it is done this way.
                try
                {
                    var AddProduct = Driver.Instance.FindElement(By.Id("Add-New-Product"));
                    var h3s = Driver.Instance.FindElements(By.TagName("h3"));
                    return h3s[0].Text;
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    return "NoSuchElementExteproion - Login Error";
                }
   
            }

        }




    }
}
