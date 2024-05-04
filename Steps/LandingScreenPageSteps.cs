using AventStack.ExtentReports.Gherkin.Model;
using FluentAssertions;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using System;
using TechTalk.SpecFlow;
using Assert = NUnit.Framework.Assert;

namespace MicroappPlatformQaAutomation
{
    [Binding]
    internal class LandingScreenPagePageSteps
    {
        private readonly EventFiringWebDriver _driver;
        private readonly static string yamlLandingScreenTestDataFilePath = "Resources/TestData/LandingScreenTestData.yaml";
        private readonly static string yamlLoginTestDataFilePath = "Resources/TestData/LoginTestData.yaml";
        private readonly JsExecutor _jsExecutor;
        private readonly ExplicitWait _explicitWait;
        string copyRight = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).Copyright;
        string EyClientsOnly = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).EyClientsOnly;
        string PrivacyStatement = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).PrivacyStatement;
        string titleOfLeftHandNavigationText = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).LeftHandNavigationText;
        String userName = YamlReader.FetchYamlData<LoginContentDTO>(yamlLoginTestDataFilePath).UserProfileName;
        private static Actions actions;
        private readonly LandingScreenPage _landingScreenPage;

        public LandingScreenPagePageSteps(EventFiringWebDriver webDriver, LandingScreenPage landingScreenPage, JsExecutor jsExecutor, ExplicitWait explicitWait)
        {
            _driver = webDriver;
            _landingScreenPage = landingScreenPage;
            _jsExecutor = jsExecutor;
            _explicitWait = explicitWait;
        }

        [Then(@"the user navigates to the signin page")]
        public void ThenTheUserNavigatesToTheSigninPage()
        {
            IWebElement welcomePage = _landingScreenPage.WelcomeToEYMicroAppPage;
            String welcomePageText = welcomePage.Text;
            string welcomeText = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).WelcomeText;
            Assert.AreEqual(welcomeText, welcomePageText);
        }

        [Given(@"User hovers to EY Branding image and validate the link with homepage link")]
        public void GivenUserHoversToEYBrandingImageAndValidateTheLinkWithHomepageLink()
        {
            actions = new Actions(_driver);
            actions.MoveToElement(_landingScreenPage.EYBrandingImageHeader).Perform();
            IWebElement microAppHeader = _landingScreenPage.MicroAppTextHeader;
            String microappTextHeader = microAppHeader.Text;
            string microAppText = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).MicroAppText;
            Assert.AreEqual(microAppText, microappTextHeader);
        }

        [When(@"user clicks the user profile icon")]
        public void WhenUserClicksTheUserProfileIcon()
        {
            _landingScreenPage.UserProfileIcon.Click();
        }

        [Then(@"the user sees dropdown with full user profile name")]
        public void ThenTheUserSeesDropdownWithFullUserProfileName()
        {
            IWebElement profileName = _landingScreenPage.FullUserProfileName;
            String fullProfileName = profileName.Text.Substring(4);
            string userProfileName = YamlReader.FetchYamlData<LandingScreenContentDTO>(yamlLandingScreenTestDataFilePath).UserProfileName;
            Assert.AreEqual(userProfileName, fullProfileName);
        }

        [Then(@"the user clicks the logout button")]
        public void ThenTheUserClicksTheLogoutButton()
        {
            try
            {
                _landingScreenPage.LogoutButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
                _driver.Navigate().Refresh();
            }
        }

        [When(@"the user scroll down to the footer")]
        public void theuserscrolldowntothefooter()
        {
            _jsExecutor.ScrollTillLast(_landingScreenPage.PrivacyLink);
        }

        [Then(@"the footer should have the image in the right side")]
        public void thefootershouldhavetheimageintherightside()
        {
            bool eyImageIsPresent = _landingScreenPage.EyPhoto.Displayed;
            Assert.That(eyImageIsPresent, Is.True);
        }

        [Then(@"the footer should have the link of Privacy Statement in the left side")]
        public void thefootershouldhavethelinkofPrivacyStatementintheleftside()
        {
            bool privacyStatement = _landingScreenPage.PrivacyLink.Displayed;
            Assert.That(privacyStatement, Is.True);
        }

        [Then(@"the footer should have copyright visible")]
        public void ThenTheFooterShouldHaveCopyrightVisible()
        {
            bool copyRightSymbol = _landingScreenPage.CopyRight.Displayed;
            Assert.That(copyRightSymbol, Is.True);
            string EYGMLimited = _landingScreenPage.CopyRight.Text;
            Assert.AreEqual(EYGMLimited, copyRight);
        }

        [Then(@"the footer should have the EYGM Limited")]
        public void thefootershouldhavetheEYGMLimited()
        {
            bool allRights = _landingScreenPage.AllRights.Displayed;
            Assert.That(allRights, Is.True);
            string limited = _landingScreenPage.AllRights.Text;
            StringAssert.Contains(copyRight, limited);
        }

        [Then(@"the footer should have the EYClients only")]
        public void thefootershouldhavetheEYClientsonly()
        {
            bool eyClients = _landingScreenPage.EYClients.Displayed;
            Assert.That(eyClients, Is.True);
            String verifyEY = _landingScreenPage.EYClients.Text;
            Assert.AreEqual(verifyEY, EyClientsOnly);
        }

        [When(@"the user click on Privacy Statement link")]
        public void theuserclickonPrivacyStatementlink()
        {
            _landingScreenPage.PrivacyLink.Click();
        }

        [Then(@"privacy statement link should open a new tab")]
        public void privacystatementlinkshouldopenanewtab()
        {
            string parentWindowHandle = _driver.CurrentWindowHandle;
            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            bool privacyTitle = _landingScreenPage.PrivacyTitle.Displayed;
            Assert.That(privacyTitle, Is.True);
            Assert.AreEqual(_landingScreenPage.PrivacyTitle.Text, PrivacyStatement);
            _driver.SwitchTo().Window(parentWindowHandle);
        }

        [When(@"the user scroll down to see view more")]
        public void theuserscrolldowntoseeviewmore()
        {
            _jsExecutor.ScrollTillLast(_landingScreenPage.ViewMoreLink);
        }

        [When(@"the user select""(.*)""from catalog items")]
        public void theuserselectfromcatalogitems(string item)
        {
            int numberOfCatalogItems = _landingScreenPage.CatlogItems.Count;
            try
            {
                for (int i = 0; i <= numberOfCatalogItems; i++)
                {
                    String itemNeeded = _landingScreenPage.CatlogItems[i].Text.ToLower();
                    if (itemNeeded.Equals(item.ToLower()))
                    {
                        _explicitWait.Until(_landingScreenPage.ViewMoreButtons[i]);
                        _landingScreenPage.ViewMoreButtons[i].Click();
                        break;
                    }
                }
            }
            catch (StaleElementReferenceException e)
            {
            }
        }

        [Then(@"the title of left hand navigation is visible")]
        public void thetitleoflefthandnavigationisvisible()
        {
            bool leftHandTitle = _landingScreenPage.LeftHnadNavigationTitle.Displayed;
            Assert.That(leftHandTitle, Is.True);
            Assert.AreEqual(_landingScreenPage.LeftHnadNavigationTitle.Text, titleOfLeftHandNavigationText);
        }

        [Then(@"the general description tab should be selected by default")]
        public void thegeneraldescriptiontabshouldbeselectedbydefault()
        {
            bool genralDescriptionTabIsSelected = _landingScreenPage.GeneralDescription.Displayed;
            Assert.That(genralDescriptionTabIsSelected, Is.True);
            bool overViewIsPresent = _landingScreenPage.OverViewText.Displayed;
            Assert.That(overViewIsPresent, Is.True);
            String generalDescriptionAttribute = _landingScreenPage.GeneralDescription.GetAttribute("aria-selected");
            Assert.AreEqual(generalDescriptionAttribute, "true");
            String tryOutAttribute = _landingScreenPage.TryOut.GetAttribute("aria-selected");
            Assert.AreEqual(tryOutAttribute, "false");
        }

        [When(@"the user click on try out tab")]
        public void theuserclickontryouttab()
        {
            _jsExecutor.JSClick(_landingScreenPage.TryOut);
        }

        [Then(@"try out page should be present and the highlight  should be under try out tab")]
        public void tryoutpageshouldbepresentandthighlightshouldbeundertryouttab()
        {
            String tryOutAttribute = _landingScreenPage.TryOut.GetAttribute("aria-selected");
            Assert.AreEqual(tryOutAttribute, "true");
            String generalDescriptionAttribute = _landingScreenPage.GeneralDescription.GetAttribute("aria-selected");
            Assert.AreEqual(generalDescriptionAttribute, "false");
        }

        //using alwyas for clicking on back button in the browser itself
        [When(@"the user click on browser back button")]
        public void theuserclickonbrowserbackbutton()
        {
            _driver.Navigate().Back();
        }

        [When(@"the user scroll down to assets section")]
        public void theuserscrolldowntoassetssection()
        {
            _jsExecutor.ScrollTillLast(_landingScreenPage.Assets);
        }

        [Then(@"request access should include four applications")]
        public void requestaccessshouldincludefourapplications()
        {
            _landingScreenPage.RequestAccess.Count.Should().Be(4);
        }
        [When(@"the user in the home page and click on EY icon")]
        public void theuserinthehomepageandclickonEYicon()
        {
            try
            {
                _landingScreenPage.EYTopLOGO.Click();
            }
            catch (StaleElementReferenceException e)
            {
            }
        }
        [Then(@"the user back to welcome page again")]
        public void theuserbacktowelcomepageagain()
        {
            String welcomemessage = _landingScreenPage.WelcomeUser.Text;
            Assert.AreEqual(welcomemessage, "Welcome," + " " + userName);
        }
    }
}