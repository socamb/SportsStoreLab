
using OpenQA.Selenium;
using System.Threading;

namespace SportsStoreAutomation
{

    /// <summary>
    /// This uses an approach for make the API Easy. It uses static methods and 
    /// OOP Constructs so developing Unit Tests are easy
    /// </summary>
    public class LoginPage
    {
        // This navigates to the site. There will be a drive for each type of Browser.
        public static void GoTo(string Url)
        {
            Driver.Instance.Navigate().GoToUrl(Url);
        }

        // We start with LoginAs. It uses the LoginCommand class designed in a way
        // for easy consumption by the unit test project.
        public static LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }

    }

    // This allows any user/password to login to the admin page.
    public class LoginCommand
    {
        private readonly string userName;
        private string password;

        public LoginCommand(string userName)
        {
            this.userName = userName;
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        // This is the actual code that acceses the webpage using Selenium WebDriver
        public void Login()
        {
            var loginInput = Driver.Instance.FindElement(By.Id("UserName"));
            loginInput.SendKeys(userName);

            var passwordInput = Driver.Instance.FindElement(By.Id("Password"));
            passwordInput.SendKeys(password);

            var loginButton = Driver.Instance.FindElement(By.Id("admin-submit"));
            loginButton.Click();

            // There are better ways to do this.
            Thread.Sleep(1000);

        }

    }
}
