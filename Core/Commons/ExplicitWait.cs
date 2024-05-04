using MicroappPlatformQaAutomation.Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    public class ExplicitWait
    {
        private readonly EventFiringWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly TestConfiguration _config;

        public ExplicitWait(EventFiringWebDriver driver)
        {
            _driver = driver;
            _config = ConfigurationReader.GetTestConfiguration();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_config.Timeouts.ImplicitWaitTimeoutInSeconds));
        }

        public void WaitForPageLoadComplete()
        {
            var js = (IJavaScriptExecutor)_driver;
            _wait.Until(driver => js.ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void WaitForVisibilityOfElement(By element)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(element));
        }

        public void WaitForPresenceOfElement(By element)
        {
            _wait.Until(ExpectedConditions.ElementExists(element));
        }

        public void WaitForElementToBeClickable(By element)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForElementToBeSelected(By element)
        {
            _wait.Until(ExpectedConditions.ElementToBeSelected(element));
        }

        public void WaitForInvisibilityOfElement(By element)
        {
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
        }

        public void Until(IWebElement element)
        {
            _wait.Until(e => element);
        }

        public void Until(IWebElement element, long timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(e => element);
        }
    }
}
