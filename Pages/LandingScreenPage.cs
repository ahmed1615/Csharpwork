using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System.Collections.Generic;

namespace MicroappPlatformQaAutomation.Pages
{
    public class LandingScreenPage
    {
        private readonly EventFiringWebDriver _driver;

        public LandingScreenPage(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement EYBrandingImageHeader => _driver.FindElement(By.XPath("//img[@alt='EY Logo']"));
        public IWebElement EYTopLOGO => _driver.FindElement(By.CssSelector("div.motif-header-logo"));
        public IWebElement WelcomeUser => _driver.FindElement(By.CssSelector("div.title"));
        public IWebElement MicroAppTextHeader => _driver.FindElement(By.XPath("//span[text()='MicroApp']"));
        public IWebElement LogoutButton => _driver.FindElement(By.XPath("//button[@aria-label='Logout']"));
        public IWebElement EyPhoto => _driver.FindElement(By.XPath("//div[@class='left-content']/img"));
        public IWebElement PrivacyLink => _driver.FindElement(By.LinkText("Privacy Statement"));
        public IWebElement CopyRight => _driver.FindElement(By.XPath("//div[contains(text(),'©')]"));
        public IWebElement AllRights => _driver.FindElement(By.CssSelector(".description-list>div:nth-child(1)"));
        public IWebElement EYClients => _driver.FindElement(By.CssSelector(".description-list>div:nth-child(2)"));
        public IWebElement WelcomeToEYMicroAppPage => _driver.FindElement(By.XPath("//h1[text()='Welcome to EY MicroApps']"));
        public IWebElement PrivacyTitle => _driver.FindElement(By.XPath("//h1[contains(text(),'Privacy statement')]"));
        public IWebElement FullUserProfileName => _driver.FindElement(By.CssSelector("button[class^='motif-dropdown']"));
        public IWebElement UserProfileIcon => _driver.FindElement(By.Id("dropdown-trigger-1"));
        //Catalog Description
        public IList<IWebElement> CatlogItems => _driver.FindElements(By.XPath("//li[@class='motif-carousel-item']/div/div/header"));
        public IWebElement ViewMoreLink => _driver.FindElement(By.XPath("//button[text()='View more']"));
        public IList<IWebElement> ViewMoreButtons => _driver.FindElements(By.XPath("//button[@class='motif-text-alt-button ']"));
        public IWebElement LeftHnadNavigationTitle => _driver.FindElement(By.XPath("//h1[contains(text(),'Left Hand Navigation')]"));
        public IWebElement GeneralDescription => _driver.FindElement(By.XPath("//button[contains(text(),'General Description')]"));
        public IWebElement TryOut => _driver.FindElement(By.XPath("//button[contains(text(),'Try Out')]"));
        public IWebElement OverViewText => _driver.FindElement(By.XPath("//div[contains(text(),'Overview')]"));
        public IList<IWebElement> RequestAccess => _driver.FindElements(By.CssSelector("div[class='asset-box']"));
        public IWebElement Assets => _driver.FindElement(By.XPath("//div[contains(text(),'Assets')]"));
    }
}