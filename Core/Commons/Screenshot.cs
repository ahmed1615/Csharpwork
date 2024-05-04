using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    public class Screenshot
    {
        private readonly EventFiringWebDriver _driver;

        public Screenshot(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public MediaEntityModelProvider CaptureScreenshotAndReturnModel(string Name)
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, Name).Build();
        }
    }
}
