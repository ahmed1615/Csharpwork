using MicroappPlatformQaAutomation.Core.Commons;
using OpenQA.Selenium;

namespace MicroappPlatformQaAutomation.Core.Extensions
{
    public static class WebDriverExtension
    {
        public static void HighlightElement(this IWebDriver driver, By by)
        {
            new JsExecutor(driver).HighlightElement(by);
        }

        public static void HighlightElement(this IWebDriver driver, IWebElement element)
        {
            new JsExecutor(driver).HighlightElement(element);
        }
    }
}
