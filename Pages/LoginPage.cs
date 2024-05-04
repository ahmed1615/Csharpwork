using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace MicroappPlatformQaAutomation.Pages
{
    public class LoginPage
    {
        private readonly EventFiringWebDriver _driver;

        public LoginPage(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement SignInButton => _driver.FindElement(By.CssSelector("button[type = 'submit']"));
        public IWebElement EmailTextbox => _driver.FindElement(By.CssSelector("input[type='email']"));
        public IWebElement SignInNextButton => _driver.FindElement(By.CssSelector("form button[type='submit']"));
        public IWebElement AcceptCookiesButton => _driver.FindElement(By.XPath("//button[text()='Accept Cookies']"));
        public IWebElement ExploreCatalogButton => _driver.FindElement(By.XPath("//a[text()='Explore Catalog']"));
        public IWebElement UserProfileIcon => _driver.FindElement(By.Id("dropdown-trigger-1"));
        public IWebElement UserProfileLabel => _driver.FindElement(By.CssSelector("button[aria-label='User Profile']"));
        public IWebElement FullUserProfileName => _driver.FindElement(By.CssSelector("button[class^='motif-dropdown']"));
        public IWebElement UserProfileName => _driver.FindElement(By.CssSelector("button[class^='motif-dropdown']"));
        public IWebElement LoginErrorMessage => _driver.FindElement(By.CssSelector("p.motif-error-message "));
    }
}