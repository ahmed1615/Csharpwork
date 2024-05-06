using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroappPlatformQaAutomation.Pages
{
    public class LoginPageDemo {

        private readonly EventFiringWebDriver _driver;
        public LoginPageDemo(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement userName => _driver.FindElement(By.Id("user-name"));
        public IWebElement password => _driver.FindElement(By.Id("password"));
        public IWebElement loginButton => _driver.FindElement(By.XPath("//input[@type='submit']"));

        public IWebElement logo => _driver.FindElement(By.XPath("//div[@class='app_logo']"));

    }
}
