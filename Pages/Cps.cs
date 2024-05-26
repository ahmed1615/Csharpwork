using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroappPlatformQaAutomation.Pages
{
    public class Cps
    {
        private readonly EventFiringWebDriver _driver;

        public Cps(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement cotactUs => _driver.FindElement(By.XPath("//div[@class='transparent-cta-alt regular']/a"));
        public IWebElement acceptCookis => _driver.FindElement(By.Id("hs-eu-confirmation-button"));
         
        //frame
        public IWebElement frameForm => _driver.FindElement(By.XPath("//iframe[@frameborder='0']"));
        public IWebElement firstName => _driver.FindElement(By.XPath("(//input[@type='text'])[1]"));
        public IWebElement lastName => _driver.FindElement(By.XPath("(//input[@type='text'])[2]"));
        public IWebElement email => _driver.FindElement(By.XPath("(//input[@type='text'])[3]"));
        public IWebElement organization => _driver.FindElement(By.XPath("(//input[@type='text'])[4]"));

        public IWebElement phone => _driver.FindElement(By.XPath("(//input[@type='text'])[5]"));
        public IWebElement jobTitle => _driver.FindElement(By.XPath("(//select[@class='select'])[1]"));
        public IWebElement industry => _driver.FindElement(By.XPath("(//select[@class='select'])[2]"));
        public IWebElement state => _driver.FindElement(By.XPath("(//select[@class='select'])[3]"));
        public IWebElement primaryIntrest => _driver.FindElement(By.XPath("(//select[@class='select'])[4]"));
        public IWebElement comment => _driver.FindElement(By.XPath("//textarea[@class='standard']"));

        //repotframe
        public IWebElement rebotframe => _driver.FindElement(By.XPath("//iframe[@title='reCAPTCHA']"));
        public IWebElement checkboxRobot => _driver.FindElement(By.Id("recaptcha-anchor"));
        public IWebElement submitButton => _driver.FindElement(By.XPath("//input[@type='submit']"));




    }
}
