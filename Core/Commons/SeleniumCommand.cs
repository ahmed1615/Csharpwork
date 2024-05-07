using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    public class SeleniumCommand
    {
        private readonly IWebDriver _driver;

        public SeleniumCommand(IWebDriver driver)
        {
            _driver = driver;
        }
        public  void clickelement(IWebElement element) {
            try { 
                if(element != null &element.Enabled) {
                    element.Click();               }
            }
            catch(StaleElementReferenceException ex) { }
        }
    }
}
