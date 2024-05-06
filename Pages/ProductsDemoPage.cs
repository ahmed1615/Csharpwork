using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroappPlatformQaAutomation.Pages
{
    public class ProductsDemoPage
    {
        private readonly EventFiringWebDriver _driver;
        public ProductsDemoPage(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement selectSorte => _driver.FindElement(By.ClassName("product_sort_container"));
        public IList<IWebElement> allProducts => _driver.FindElements(By.XPath("//div[@class='inventory_item_name ']"));
        public IList<IWebElement> blockofproduct => _driver.FindElements(By.XPath("//div[@class='inventory_item_description']"));

        public IWebElement productName => selectSorte.FindElement(By.CssSelector("div[class='inventory_item_description'] a div"));
        public IWebElement addCard => _driver.FindElement(By.CssSelector("div[class='inventory_item_description'] button"));
        public IWebElement card => _driver.FindElement(By.Id("shopping_cart_container"));
        public IWebElement itemPrice => _driver.FindElement(By.XPath("//div[@class='inventory_item_price']"));
        public IWebElement checkoutButton => _driver.FindElement(By.Id("checkout"));
        public IWebElement firstName => _driver.FindElement(By.Id("first-name"));
        public IWebElement lastName => _driver.FindElement(By.Id("last-name"));
        public IWebElement postCode => _driver.FindElement(By.Id("postal-code"));
        public IWebElement contuineButton => _driver.FindElement(By.Id("continue"));
        public IWebElement finishButton => _driver.FindElement(By.Id("finish"));
        public IWebElement sucussMessage => _driver.FindElement(By.TagName("h2"));




    }
}
