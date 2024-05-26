using AventStack.ExtentReports.Gherkin.Model;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Core.Config.Parameters;
using MicroappPlatformQaAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MicroappPlatformQaAutomation.Steps
{
    [Binding]
    internal class CPSSteps
    {
        private readonly ExplicitWait _explicitWait;
        private readonly Selectelementby _select;
        private readonly Cps _cps;
        private readonly EventFiringWebDriver _driver;
        private readonly JsExecutor _jsExecutor;
        public CPSSteps(Cps cps, ExplicitWait explicitWait, EventFiringWebDriver webDriver, Selectelementby select, JsExecutor js
            )
        {
            _driver = webDriver;
            _cps = cps;
            _explicitWait = explicitWait;
            _select = select;
            _jsExecutor = js;
        }
        [When(@"the user click on contact us")]
        public void the_user_click_on_contact_us()
        {
            try
            {
                _cps.cotactUs.Click();
            }
            catch (StaleElementReferenceException e) { 
            }
        }
        [When(@"the user accept cookis")]
        public void the_user_accept_cookis()
        {
            _cps.acceptCookis.Click();
        }
        [When(@"the user wants to complete the form")]
        public void the_user_wants_to_complete_the_form()
        {
            Thread.Sleep(3000);
            _driver.SwitchTo().Frame(_cps.frameForm);
            _driver.SwitchTo().Frame(_cps.rebotframe);
            _jsExecutor.JSClick(_cps.checkboxRobot);
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(_cps.frameForm);
            _cps.firstName.SendKeys("jhjb");
            _cps.lastName.SendKeys("jbj");
            _cps.email.SendKeys("bjj");
            _cps.organization.SendKeys("alallahelhkatya");
            _cps.phone.SendKeys("1190875643");
            _select.selectByText(_cps.jobTitle, "Manager");
            _select.selectByText(_cps.industry, "Clinic/Center");
            _select.selectByText(_cps.state, "Arizona");
            _select.selectByText(_cps.primaryIntrest, "Consulting");
            _cps.comment.SendKeys("comment");
            _jsExecutor.JSClick(_cps.submitButton);





        }
    }
}
