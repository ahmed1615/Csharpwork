using MicroappPlatformQaAutomation.Core.Config;
using MicroappPlatformQaAutomation.Core.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;

namespace MicroappPlatformQaAutomation.Core.Driver.Listeners
{
    public class DriverDebugListener
    {
        private readonly TestConfiguration _configuration;

        public DriverDebugListener(TestConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Navigating(object sender, WebDriverNavigationEventArgs e)
        {
            Console.WriteLine($"* Navigating to: {e.Url}");
        }

        public void ElementClicking(object sender, WebElementEventArgs e)
        {
            Console.WriteLine($"* Clicking on element: {e.Element}");

            if (_configuration.Webdriver.IsElementHighlightActive)
            {
                e.Driver.HighlightElement(e.Element);
            }
        }

        public void ElementClicked(object sender, WebElementEventArgs e)
        {
            Console.WriteLine("* Element clicked");

            if (_configuration.Webdriver.IsElementHighlightActive)
            {
                e.Driver.HighlightElement(e.Element);
            }
        }

        public void ElementValueChanging(object sender, WebElementValueEventArgs e)
        {
            Console.WriteLine($"* Changing element value: {e.Element}");
        }

        public void ElementValueChanged(object sender, WebElementValueEventArgs e)
        {
            Console.WriteLine("* Element value changed");

            if (_configuration.Webdriver.IsElementHighlightActive)
            {
                e.Driver.HighlightElement(e.Element);
            }
        }

        public void FindingElement(object sender, FindElementEventArgs e)
        {
            Console.WriteLine($"** Finding element: {e.FindMethod}");

            if (_configuration.Webdriver.IsElementHighlightActive)
            {
                e.Driver.HighlightElement(e.FindMethod);
            }
        }

        public void FindElementCompleted(object sender, FindElementEventArgs e)
        {
            Console.WriteLine("** Finding element completed");
        }

        public void ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_hhmm");
            ((ITakesScreenshot)e.Driver).GetScreenshot().SaveAsFile("error_" + timestamp + ".png");
        }

        internal void ScriptExecuted(object sender, WebDriverScriptEventArgs e)
        {
            Console.WriteLine("* Script executed");
        }
    }
}
