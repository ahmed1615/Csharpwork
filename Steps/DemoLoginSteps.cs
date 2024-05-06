using AventStack.ExtentReports.Gherkin.Model;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using SeleniumExtras.WaitHelpers;
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
    internal class DemoLoginSteps
    {
        
        private readonly static string yamlDemoLoginTestDataFilePath = "Resources/TestData/DemoTestData.yaml";
        private readonly ExplicitWait _explicitWait;
        private readonly LoginPageDemo _LoginDemo;
        public DemoLoginSteps( LoginPageDemo loginDemo, ExplicitWait explicitWait)
        {
            _LoginDemo = loginDemo;
            _explicitWait = explicitWait;
        }
        [Given(@"the user fill username")]
        public void WhenTheUserFillUsername()
        {

            string us = YamlReader.FetchYamlData<DemologinDTO>(yamlDemoLoginTestDataFilePath).username;
            _LoginDemo.userName.SendKeys(us);

        }

      
        [When(@"the user fill the password")]
        public void WhenTheUserFillThePassword()
        {
            string pw = YamlReader.FetchYamlData<DemologinDTO>(yamlDemoLoginTestDataFilePath).password;
            _LoginDemo.password.SendKeys(pw);
        }

    
        [When(@"the user click on login button")]
        public void WhenTheUserClickOnLoginButton()
        {
            try
            {
                _LoginDemo.loginButton.Click();
            }
            catch (StaleElementReferenceException e) { 
            }
        }

     
        [Then(@"homepage should appear")]
        public void ThenHomepageShouldAppear()
        {
            
            string LOGOTEXT = YamlReader.FetchYamlData<DemologinDTO>(yamlDemoLoginTestDataFilePath).logoname;

            _explicitWait.WaitForPageLoadComplete();
            
            String logoText = _LoginDemo.logo.Text;

            Assert.AreEqual(LOGOTEXT, logoText);
 

        }
    }
}
