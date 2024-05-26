using MicroappPlatformQaAutomation.Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    public class SeleniumCommand
    {
        private readonly EventFiringWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly TestConfiguration _config;

        public SeleniumCommand(EventFiringWebDriver driver)
        {
            _driver = driver;
            _config = ConfigurationReader.GetTestConfiguration();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_config.Timeouts.ImplicitWaitTimeoutInSeconds));
        }
        public  void clickelement(IWebElement element) {
            try { 
                if(element != null &element.Enabled) {
                    element.Click();              
                }
            }
            catch(StaleElementReferenceException ex) { }
        }
    }
}
