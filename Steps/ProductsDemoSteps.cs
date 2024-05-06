using AventStack.ExtentReports.Gherkin.Model;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using MicroappPlatformQaAutomation.Resources.TestData;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MicroappPlatformQaAutomation.Steps
{
    [Binding]
    internal class ProductsDemoSteps
    {
        private readonly ExplicitWait _explicitWait;
        private readonly ProductsDemoPage _products;
        private readonly Selectelementby _select;
        public string priceofunit;
        public ProductsDemoSteps(ProductsDemoPage productsDemo, ExplicitWait explicitWait, Selectelementby select)
        {
            _products = productsDemo;
            _explicitWait = explicitWait;
            _select = select;

        }
        [When(@"the user get all the product from A to Z and Z to A")]
        public void the_user_get_all_the_product_from_A_to_Z_and_Z_to_A()
        {
            ArrayList a = new ArrayList();
            IList<IWebElement> allproductslist = _products.allProducts;
            foreach (IWebElement product in allproductslist)
            {
                String prodctNamesByDefaulte = product.Text;
                a.Add(prodctNamesByDefaulte);
            }

            try
            {
                _select.selectByText(_products.selectSorte, "Name (Z to A)");
            }
            catch (StaleElementReferenceException e)
            {

            }
            ArrayList b = new ArrayList();
            IList<IWebElement> sorteallproductslist = _products.allProducts;
            foreach (IWebElement product in sorteallproductslist)
            {
                String prodctNamesByDefaulte = product.Text;
                b.Add(prodctNamesByDefaulte);
            }
            b.Sort();
            Assert.AreEqual(a, b);
        }
        [When(@"the user select product ""(.*)""")]
        public void the_user_select_product(String item)
        {
            IList<IWebElement> allproductslist = _products.blockofproduct;
            foreach (IWebElement product in allproductslist)
            {
                if (product.FindElement(By.CssSelector("div[class='inventory_item_description'] a div")).Text.ToLower().Contains(item.ToLower()))
                {
                    string priceofunit = product.FindElement(By.CssSelector("div[class='inventory_item_price']")).Text;
                    this.priceofunit = priceofunit.ToString();
                    Console.WriteLine(priceofunit);
                    try
                    {
                        product.FindElement(By.CssSelector("div[class='inventory_item_description'] button")).Click();
                    }
                    catch (StaleElementReferenceException e) { }
                }
            }
        }
        [When(@"the user check the card")]
        public void the_user_check_the_card()
        {
            try
            {
                _products.card.Click();
            }
            catch (StaleElementReferenceException e) { }
            String itemprice= _products.itemPrice.Text;
            Assert.AreEqual(priceofunit, itemprice);
            try {
                _products.checkoutButton.Click();
            } catch (StaleElementReferenceException e) { }
            _products.firstName.SendKeys(Genrator.GetRandomName());
            _products.lastName.SendKeys(Genrator.GetRandomName());
            _products.postCode.SendKeys("1234");
            try {
                _products.contuineButton.Click();
               
            }
            catch (StaleElementReferenceException e) { }
            try {
                _products.finishButton.Click();
            } 
            catch (StaleElementReferenceException e) { }
            String message = _products.sucussMessage.Text;
            Assert.AreEqual(message, "Thank you for your order!");
        }

    }
}
   
