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

        // This adds a product to toe Product Catalog.
        public static void AddNewProduct(string Name, string Description, 
            string Price, string Category)
        {
    
            var addButton = Driver.Instance.FindElement(By.Id("AddNewProduct"));
            addButton.Click();

            var nameInput = Driver.Instance.FindElement(By.Id("Name"));
            nameInput.SendKeys(Name);

            var descriptionInput = Driver.Instance.FindElement(By.Id("Description"));
            descriptionInput.SendKeys(Name);

            var priceInput = Driver.Instance.FindElement(By.Id("Price"));
            priceInput.Clear();
            priceInput.SendKeys(Price);

            var categoryInput = Driver.Instance.FindElement(By.Id("Category"));
            categoryInput.SendKeys(Category);

            var saveButton = Driver.Instance.FindElement(By.Id("SaveProduct"));
            saveButton.Click();
        }

    
        // If we are on the Maintenance Page, this element = "All Products"
        public static string AllProducts
        {
            get
            {
                try
                {
                    var h3s = Driver.Instance.FindElements(By.TagName("h3"));
                    return h3s[0].Text;
                }
                // If the element is not found, this exception is thrown. This will cause the assert to fail 
                // in the test
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    return "NoSuchElementException - Login Error";
                }
           }
        }




    }
}
