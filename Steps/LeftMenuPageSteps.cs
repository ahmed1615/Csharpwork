using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TechTalk.SpecFlow;
using Assert = NUnit.Framework.Assert;

namespace MicroappPlatformQaAutomation
{
    [Binding]
    internal class LeftMenuPageSteps
    {
        private readonly EventFiringWebDriver _driver;
        private readonly ExplicitWait _explicitWait;
        private readonly JsExecutor _jsExecutor;
        private readonly static string yamlTestDataFilePath = "Resources/TestData/LeftMenuTestData.yaml";
        List<string> expectedCatalogList = YamlReader.FetchYamlData<LeftMenuContentDTO>(yamlTestDataFilePath).CatalogList;
        List<string> expectedGuidelinesList = YamlReader.FetchYamlData<LeftMenuContentDTO>(yamlTestDataFilePath).GuidelinesList;
        List<string> expectedFoundationList = YamlReader.FetchYamlData<LeftMenuContentDTO>(yamlTestDataFilePath).FoundationList;
        List<string> expectedDesignPatternsList = YamlReader.FetchYamlData<LeftMenuContentDTO>(yamlTestDataFilePath).DesignPatternsList;
        List<string> expectedFrameworkLibrariesList = YamlReader.FetchYamlData<LeftMenuContentDTO>(yamlTestDataFilePath).FrameworkLibrariesList;
        private readonly LeftMenuPage _leftMenuPage;

        public LeftMenuPageSteps(EventFiringWebDriver webDriver, LeftMenuPage leftMenuPage, ExplicitWait explicitWait, JsExecutor jsExecutor)
        {
            _driver = webDriver;
            _leftMenuPage = leftMenuPage;
            _explicitWait = explicitWait;
            _jsExecutor = jsExecutor;
        }

        [Given(@"User clicks on hamburger menu")]
        public void GivenUserClickOnHamburgerMenu()
        {
            _explicitWait.Until(_leftMenuPage.HamburgerMenuButton);
            _leftMenuPage.HamburgerMenuButton.Click();
        }

        [Then(@"the user sees the extended menu of left navigation bar")]
        public void ThenTheUserSeesTheExtendedMenuOfLeftNavigationBar()
        {
            _explicitWait.Until(_leftMenuPage.ExtendedLeftNavigationBar);
            bool ExtendedMenuDisplay = _leftMenuPage.ExtendedLeftNavigationBar.Displayed;
            Assert.That(ExtendedMenuDisplay, Is.True);
        }

        [Then(@"the user clicks back hamburger menu it collapses")]
        public void ThenTheUserClicksBackHamburgerMenuItCollapses()
        {
            _leftMenuPage.HamburgerMenuButton.Click();
            bool displayed = _leftMenuPage.CollapsedLeftNavigationBar.Displayed;
            Assert.That(displayed, Is.True);
        }

        [Then(@"the user sees collapsed left navigation bar on initial load")]
        public void GivenTheUserSeesCollapsedLeftNavigationBarOnInitialLoad()
        {
            _explicitWait.Until(_leftMenuPage.CollapsedLeftNavigationBar);
            bool displayed = _leftMenuPage.CollapsedLeftNavigationBar.Displayed;
            Assert.That(displayed, Is.True);
        }

        [Then(@"the user sees hamburger menu at the top of left navigation bar")]
        public void WhenTheUserSeesHamburgerMenuAtTheTopOfLeftNavigationBar()
        {
            bool displayed = _leftMenuPage.HamburgerMenuButton.Displayed;
            Assert.That(displayed, Is.True);
        }

        [Then(@"the user sees the relevant text on hovering icons")]
        public void ThenTheUserSeesTheRelevantTextOnHoveringIcons()
        {

            foreach (IWebElement element in _leftMenuPage.LeftNavigationicons)
            {
                Actions actions = new Actions(_driver);
                bool iconAttributeValueDisplayed = element.Displayed;
                String iconAttribute = element.GetAttribute("aria-label");
                Assert.That(iconAttributeValueDisplayed, Is.True);
                actions.MoveToElement(element).Perform();
                bool textDisplayed = _leftMenuPage.MenuHoverText.Displayed;
                String textAfterMenuHover = _leftMenuPage.MenuHoverText.Text;
                Assert.That(textDisplayed, Is.True);
                Assert.AreEqual(iconAttribute, textAfterMenuHover);
            }
        }

        [Given(@"user clicks hamburger menu and see the behaviour of application in different screen sizes")]
        public void GivenUserClicksHamburgerMenuAndSeeTheBehaviourOfApplicationInDifferentScreenSizes()
        {
            _explicitWait.Until(_leftMenuPage.HamburgerMenuButton);
            _leftMenuPage.HamburgerMenuButton.Click();
        }

        [Then(@"user set the screen size to different resolution ""([^""]*)"" in both landscape and potrait mode")]
        public void ThenUserSetTheScreenSizeToDifferentResolutionInBothLandscapeAndPotraitMode(string tab)
        {
            if (tab == "IPad")
            {
                _driver.Manage().Window.Size = new System.Drawing.Size(820, 1180);
                _explicitWait.Until(_leftMenuPage.HamburgerMenuButton);
                bool potraitDisplay = _leftMenuPage.ExtendedLeftNavigationBar.Displayed;
                Assert.That(potraitDisplay, Is.True);
                _driver.Manage().Window.Size = new System.Drawing.Size(1180, 820);
                bool landscapeDisplay = _leftMenuPage.ExtendedLeftNavigationBar.Displayed;
                Assert.That(landscapeDisplay, Is.True);
            }
            else
            {
                _driver.Manage().Window.Size = new System.Drawing.Size(712, 1138);
                _explicitWait.Until(_leftMenuPage.HamburgerMenuButton);
                bool potraitDisplay = _leftMenuPage.ExtendedLeftNavigationBar.Displayed;
                Assert.That(potraitDisplay, Is.True);
                _driver.Manage().Window.Size = new System.Drawing.Size(1138, 712);
                bool landscapeDisplay = _leftMenuPage.ExtendedLeftNavigationBar.Displayed;
                Assert.That(landscapeDisplay, Is.True);
            }
        }

        [Then(@"the submenu items are displayed on clicking arrow in the extended menu")]
        public void ThenTheSubmenuItemsAreDisplayedOnClickingArrowInTheExtendedMenu()
        {
            foreach (IWebElement element in _leftMenuPage.MenuWithSubMenuItemsList)
            {
                element.Click();
                _explicitWait.Until(_leftMenuPage.ExpandedMenu);
                bool expandedMenuItemsDisplayed = _leftMenuPage.ExpandedMenu.Displayed;
                Assert.That(expandedMenuItemsDisplayed, Is.True);
                bool expandedMenuPortalDisplayed = _leftMenuPage.ExpandedMenuPortal.Displayed;
                Assert.That(expandedMenuPortalDisplayed, Is.True);
                String menuAttributeValue = element.GetAttribute("aria-label");
                List<String> actualExpandedSubMenusList = new List<String>();

                foreach (var item in _leftMenuPage.ExpandedSubMenuItems)
                {
                    actualExpandedSubMenusList.Add(item.Text.ToString());
                }
                if (menuAttributeValue == "Catalog")
                {
                    Assert.AreEqual(expectedCatalogList, actualExpandedSubMenusList);
                }
            }
        }

        [Then(@"the arrows are not displayed in vertical menu")]
        public void ThenTheArrowsAreNotDisplayedInVerticalMenu()
        {
            Console.WriteLine("Not found");
        }

        [Then(@"the extended menu is displayed")]
        public void ThenTheExtendedMenuIsDisplayed()
        {
            _explicitWait.Until(_leftMenuPage.ExtendedMenu);
            bool extendedMenuDisplayed = _leftMenuPage.ExtendedMenu.Displayed;
            Assert.That(extendedMenuDisplayed, Is.True);
        }

        [Then(@"the scrollbar is displayed on clicking arrow in the extended menu")]
        public void ThenTheScrollbarIsDisplayedOnClickingArrowInTheExtendedMenu()
        {
            foreach (IWebElement element in _leftMenuPage.MenuArrowIconList)
            {
                element.Click();
                _explicitWait.Until(_leftMenuPage.ExtendedMenuScrollbar);
                bool extendedMenuScrollBarDisplayed = _leftMenuPage.ExtendedMenuScrollbar.Displayed;
                Assert.That(extendedMenuScrollBarDisplayed, Is.True);
            }
        }

        [Then(@"submenu items are displayed on hovering menu items")]
        public void ThenSubmenuItemsAreDisplayedOnHoveringMenuItems()
        {
            foreach (IWebElement element in _leftMenuPage.MenuWithSubMenuItemsList)
            {
                Actions actions = new Actions(_driver);
                actions.MoveToElement(element).Perform();
                bool subMenuItemsDisplayed = _leftMenuPage.SubMenuPortal.Displayed;
                Assert.That(subMenuItemsDisplayed, Is.True);
            }
        }

        [Then(@"user verifies the list of submenu items on hovering menu items")]
        public void ThenUserVerifiesTheListOfSubmenuItemsOnHoveringMenuItems()
        {
            foreach (IWebElement element in _leftMenuPage.MenuWithSubMenuItemsList)
            {
                Actions actions = new Actions(_driver);
                actions.MoveToElement(element).Perform();
                bool subMenuItemsDisplayed = _leftMenuPage.SubMenuPortal.Displayed;
                Assert.That(subMenuItemsDisplayed, Is.True);
                String menuAttributeValue = element.GetAttribute("aria-label");
                List<String> actualSubMenusList = new List<String>();

                foreach (var item in _leftMenuPage.SubMenuItems)
                {
                    actualSubMenusList.Add(item.Text.ToString());
                }
                if (menuAttributeValue == "Catalog")
                {
                    Thread.Sleep(1000);
                    Assert.AreEqual(expectedCatalogList, actualSubMenusList);
                }
                else if (menuAttributeValue == "Guidelines")
                {
                    Assert.AreEqual(expectedGuidelinesList, actualSubMenusList);
                }
                else if (menuAttributeValue == "Foundation")
                {
                    Assert.AreEqual(expectedFoundationList, actualSubMenusList);
                }
                else if (menuAttributeValue == "Design Patterns")
                {
                    Assert.AreEqual(expectedDesignPatternsList, actualSubMenusList);
                }
                else if (menuAttributeValue == "Framework Libraries")
                {
                    Assert.AreEqual(expectedFrameworkLibrariesList, actualSubMenusList);
                }
            }
        }

        [Given(@"user clicks hamburger menu")]
        public void GivenUserClicksHamburgerMenu()
        {
            _explicitWait.Until(_leftMenuPage.HamburgerMenuButton);
            _leftMenuPage.HamburgerMenuButton.Click();
        }

        [When(@"change the application window to smaller resolution")]
        public void WhenChangeTheApplicationWindowToSmallerResolution()
        {
            _driver.Manage().Window.Size = new Size(500, 300);
        }

        [Then(@"the user sees the extended menu with scroll option")]
        public void ThenTheUserSeesTheExtendedMenuWithScrollOption()
        {
            bool displayed = _leftMenuPage.HamburgerExtendedMenuScrollBar.Displayed;
            Assert.That(displayed, Is.True);
        }

        [Then(@"the user scroll till last element")]
        public void ThenTheUserScrollTillLastElement()
        {
            _jsExecutor.ScrollTillLast(_leftMenuPage.ResourcesMenuItem);
        }

        [Then(@"the submenu items are collapsed on clicking arrow again in the extended menu")]
        public void ThenTheSubmenuItemsAreCollapsedOnClickingArrowAgainInTheExtendedMenu()
        {
            foreach (var element in _leftMenuPage.MenuArrowExpanded)
            {
                element.Click();
                bool CollapsedExtendedMenuItems = _leftMenuPage.CollapsedExtendedMenu.Displayed;
                Assert.That(CollapsedExtendedMenuItems, Is.True);
            }
        }
    }
}



