using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System.Collections.Generic;

namespace MicroappPlatformQaAutomation.Pages
{
    public class LeftMenuPage
    {
        private readonly EventFiringWebDriver _driver;

        public LeftMenuPage(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement HamburgerMenuButton => _driver.FindElement(By.CssSelector("button[aria-label='Click to open vertical-navigation menu']"));
        public IList<IWebElement> LeftNavigationicons => _driver.FindElements(By.CssSelector("button[class^='motif-vertical-navigation']"));
        public IWebElement CollapsedLeftNavigationBar => _driver.FindElement(By.CssSelector("nav[class$='collapsed ']"));
        public IWebElement MenuHoverText => _driver.FindElement(By.CssSelector("div[class='motif-vertical-navigation-submenu-float-title'] , div[class='motif-vertical-navigation-menuitem-portal-label']"));
        public IList<IWebElement> ArrowIconList => _driver.FindElements(By.CssSelector("svg[class$='arrow']"));
        public IWebElement ExtendedMenu => _driver.FindElement(By.CssSelector("nav[class$='expanded-width ']"));
        public IWebElement ExtendedMenuScrollbar => _driver.FindElement(By.CssSelector("div[class$='navigation-scrolls '] button[aria-expanded='true']"));
        public IList<IWebElement> MenuArrowIconList => _driver.FindElements(By.CssSelector("div[class$='scrolls '] svg[class$='arrow']"));
        public IList<IWebElement> MenuWithSubMenuItemsList => _driver.FindElements(By.CssSelector("button[class='motif-vertical-navigation-submenu ']"));
        public IWebElement SubMenuPortal => _driver.FindElement(By.CssSelector("div[class$='motif-vertical-navigation-scrolls']"));
        public IList<IWebElement> SubMenuItems => _driver.FindElements(By.CssSelector("button[class$='navigation-submenuitem ']"));
        public IWebElement HamburgerExtendedMenuScrollBar => _driver.FindElement(By.CssSelector("div[class$='navigation-scrolls ']"));
        public IWebElement ResourcesMenuItem => _driver.FindElement(By.XPath("//div[text()='Resources']"));
        public IWebElement ExtendedLeftNavigationBar => _driver.FindElement(By.CssSelector("nav[class$='expanded-width ']"));
        public IWebElement ExpandedMenu => _driver.FindElement(By.CssSelector("div[class$='navigation-scrolls '] button[aria-expanded='true']"));
        public IWebElement ExpandedMenuPortal => _driver.FindElement(By.CssSelector("div[class$='height-auto ']"));
        public IList<IWebElement> ExpandedSubMenuItems => _driver.FindElements(By.CssSelector("div[aria-hidden='false'] button"));
        public IList<IWebElement> MenuArrowExpanded => _driver.FindElements(By.CssSelector("div[class$='scrolls '] svg[class$='arrow-open']"));
        public IWebElement CollapsedExtendedMenu => _driver.FindElement(By.CssSelector("div[class='left-nav-holder'] button[aria-expanded='false']"));
    }
}
