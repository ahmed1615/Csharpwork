using OpenQA.Selenium;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    public class JsExecutor
    {
        private readonly IWebDriver _driver;

        public JsExecutor(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ScrollToElement(By by)
        {
            var element = _driver.FindElement(by);
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void ScrollTillLast(IWebElement element)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void HighlightElement(By by)
        {
            var element = _driver.FindElement(by);
            HighlightElement(element);
        }

        public void HighlightElement(IWebElement element)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]); ",
                    element, "color: green; border: 4px solid green;");
        }

        public void JSClick(IWebElement element)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", element);
        }
    }
}
