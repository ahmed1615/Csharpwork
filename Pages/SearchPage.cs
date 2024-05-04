using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System.Collections.Generic;

namespace MicroappPlatformQaAutomation.Pages
{
    public class SearchPage
    {
        private readonly EventFiringWebDriver _driver;

        public SearchPage(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement SearchTextBox => _driver.FindElement(By.CssSelector("input[id = 'search-input']"));
        public IWebElement SearchSuggestionsBox => _driver.FindElement(By.CssSelector("ul[id='search-input-listbox']"));
        public IList<IWebElement> SearchSuggestionsList => _driver.FindElements(By.CssSelector("ul[id='search-input-listbox'] li"));
        public IWebElement ViewAllLink => _driver.FindElement(By.XPath("//div[text()='View all']"));
        public IWebElement NoResultsMessage => _driver.FindElement(By.XPath("//div[text()= 'No Results']"));
        public IWebElement SearchBar => _driver.FindElement(By.CssSelector("div[class$='field-text-input ']"));
        public IWebElement SearchIcon => _driver.FindElement(By.CssSelector("button[class$='search-input-icon-button']"));
    }
}
