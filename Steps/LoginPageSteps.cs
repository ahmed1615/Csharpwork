using AventStack.ExtentReports.Gherkin.Model;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using MicroappPlatformQaAutomation.Resources.TestData;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace MicroappPlatformQaAutomation
{
    [Binding]
    internal class LoginPageSteps
    {
        private readonly EventFiringWebDriver _driver;
        private readonly static string yamlLoginTestDataFilePath = "Resources/TestData/LoginTestData.yaml";
        private readonly ExplicitWait _explicitWait;
        private readonly JsExecutor _jsExecutor;
        string userName = YamlReader.FetchYamlData<LoginContentDTO>(yamlLoginTestDataFilePath).UserName;
        string valideName = YamlReader.FetchYamlData<LoginContentDTO>(yamlLoginTestDataFilePath).ValidName;
        string loginErrormessage = YamlReader.FetchYamlData<LoginContentDTO>(yamlLoginTestDataFilePath).LoginErrorMessage;
        private readonly LoginPage _loginPage;

        public LoginPageSteps(EventFiringWebDriver webDriver, LoginPage loginPage, ExplicitWait explicitWait, JsExecutor jsExecutor)
        {
            _driver = webDriver;
            _loginPage = loginPage;
            _explicitWait = explicitWait;
            _jsExecutor = jsExecutor;
        }

        [Given(@"the user accepts the cookies")]
        public void GivenTheUserAcceptsTheCookies()
        {
            if (_loginPage.AcceptCookiesButton.Displayed)
            {
                var js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].click();", _loginPage.AcceptCookiesButton);
            }
        }

        [Then(@"the user is logged in")]
        public void ThenTheUserIsLoggedIn()
        {
            _explicitWait.Until(_loginPage.UserProfileLabel);
            bool userProfileLabel = _loginPage.UserProfileLabel.Displayed;
            Assert.That(userProfileLabel, Is.True);
            _explicitWait.WaitForPageLoadComplete();
            try
            {
                _jsExecutor.ScrollTillLast(_loginPage.ExploreCatalogButton);
                _jsExecutor.JSClick(_loginPage.ExploreCatalogButton);
            }
            catch (StaleElementReferenceException e)
            {
            }
        }

        [Given(@"User clicks on the user profile icon")]
        public void GivenUserClicksOnTheUserProfileIcon()
        {
            _loginPage.UserProfileIcon.Click();
        }

        [Then(@"the user sees the username from the dropdown and click on it")]
        public void ThenTheUserSeesTheUsernameFromTheDropdownAndClickOnIt()
        {
            String fullProfileName = _loginPage.FullUserProfileName.Text.Substring(4);
            String userProfileName = YamlReader.FetchYamlData<LoginContentDTO>(yamlLoginTestDataFilePath).UserProfileName;
            Assert.AreEqual(userProfileName, fullProfileName);
            {
                Actions actions = new Actions(_driver);
                actions.MoveToElement(_loginPage.UserProfileName).Click().Perform();
            }
        }

        [When(@"the user login into the application")]
        public void WhenTheUserLoginIntoTheApplication()
        {
            try
            {
                _loginPage.SignInButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
            }
            if (_loginPage.EmailTextbox.GetAttribute("value") == null)
            {
                _loginPage.EmailTextbox.Click();
                _loginPage.EmailTextbox.SendKeys(userName);
            }
            else
            {
                _loginPage.EmailTextbox.Click();
                Actions actions = new Actions(_driver);
                actions.KeyDown(Keys.Control).SendKeys("a").SendKeys(Keys.Delete).Build().Perform();
                _loginPage.EmailTextbox.SendKeys("");
                _loginPage.EmailTextbox.SendKeys(userName);
            }
            try
            {
                _loginPage.SignInNextButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
                _driver.Navigate().Refresh();
            }
        }

        [When(@"the user wants to login without email")]
        public void theuserwantstologinwithoutemail()
        {
            try
            {
                _loginPage.SignInButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
            }
            _loginPage.EmailTextbox.Click();
            _loginPage.SignInNextButton.Click();
        }

        [Then(@"the focus class element should be appear and user can not see the main page")]
        public void thefocusclasselementshouldbeappearandusercannotseethemainpage()
        {
            bool NextButtonstillPresent = _loginPage.SignInNextButton.Displayed;
            Assert.That(NextButtonstillPresent, Is.True);
        }

        [When(@"the user wants to login with invalid name")]
        public void WhenTheUserWantsToLoginWithInvalidName()
        {
            try
            {
                _loginPage.SignInButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
            }
            _loginPage.EmailTextbox.Click();
            _loginPage.EmailTextbox.SendKeys(Genrator.GetRandomName());
            _loginPage.SignInNextButton.Click();
        }

        [When(@"the user wants to login with valid name with out using @ symbol")]
        public void WhenTheUserWantsToLoginWithvalidName()
        {
            try
            {
                _loginPage.SignInButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
            }
            _loginPage.EmailTextbox.Click();
            Thread.Sleep(2000);

            _loginPage.EmailTextbox.SendKeys(valideName);

            _loginPage.SignInNextButton.Click();
        }
        [When(@"the user wants to login with invalid email")]
        public void TheUserWantsToLoginWithInvalidEmail()
        {
            try
            {
                _loginPage.SignInButton.Click();
            }
            catch (StaleElementReferenceException e)
            {
            }
            _loginPage.EmailTextbox.Click();
            _loginPage.EmailTextbox.SendKeys(Genrator.GetRandomEmail());
            _loginPage.SignInNextButton.Click();
        }

        [Then(@"Login error message should be appear")]
        public void Loginerrormessageshouldbeappear()
        {
            bool NextButtonstillPresent = _loginPage.LoginErrorMessage.Displayed;
            Assert.That(NextButtonstillPresent, Is.True);
            string errorText = _loginPage.LoginErrorMessage.Text;
            Assert.AreEqual(errorText, loginErrormessage);
        }
    }
}