using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    public class Selectelementby
    {
        private readonly EventFiringWebDriver _driver;
       
        public Selectelementby(EventFiringWebDriver driver)
        {
            _driver = driver;
           

        }
        public void selectByText(IWebElement element, String text)
        {
            
            if (element != null & element.Displayed.Equals(true))
            {
                var selectElement = new SelectElement(element);
                selectElement.SelectByText(text);
            }
           
        }
        public void selectByValue(IWebElement element, String value)
        {

            if (element != null & element.Displayed.Equals(true))
            {
                var selectElement = new SelectElement(element);
                selectElement.SelectByValue(value);
            }
        }
        public void selectbyindex(IWebElement element, int index)
        {

            if (element != null & element.Displayed.Equals(true))
            {
                var selectElement = new SelectElement(element);
                selectElement.SelectByIndex(index);
            }
        }


    }
}
