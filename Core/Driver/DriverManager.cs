using MicroappPlatformQaAutomation.Core.Config;
using MicroappPlatformQaAutomation.Core.Driver.Listeners;
using MicroappPlatformQaAutomation.Core.Driver.WebdriverConfig;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.Events;
using System;
using WebDriverManager.DriverConfigs.Impl;

namespace MicroappPlatformQaAutomation.Core.Driver
{
    internal class DriverManager
    {
        private readonly TestConfiguration _config;
        private readonly EventFiringWebDriver _driver;
        private const string CHROME_BROWSER = "chrome";
        private const string EDGE_BROWSER = "edge";
        private const string BINARY_FOLDER = "binary";
        private const string CI_PARAMETER = "ci";
        private const string AUTO_PARAMETER = "auto";
        private const string MANUAL_PARAMETER = "manual";

        public DriverManager()
        {
            _config = ConfigurationReader.GetTestConfiguration();
            _driver = InitializeDriver();
            SetTimeouts();
        }

        public EventFiringWebDriver GetDriver()
        {
            return _driver;
        }

        private EventFiringWebDriver InitializeDriver()
        {
            var browser = _config.Webdriver.Browser;
            IWebDriver driver;

            switch (browser)
            {
                case CHROME_BROWSER:
                    driver = setupChrome();
                    break;
                case EDGE_BROWSER:
                    driver = setupEdge();
                    break;
                default:
                    throw new Exception($"Invalid browser value: {browser}");
            }

            EventFiringWebDriver eventFiringWebdriver = new EventFiringWebDriver(driver);
            RegisterListeners(eventFiringWebdriver);
            return eventFiringWebdriver;
        }

        private ChromeDriver setupChrome()
        {
            var type = _config.Webdriver.Type;
            switch (_config.Webdriver.Type)
            {
                case CI_PARAMETER:
                    return new ChromeDriver(BINARY_FOLDER, GetChromeOptions());
                case MANUAL_PARAMETER:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeDriverConfig(_config), _config.Webdriver.DriverVersion);
                    return new ChromeDriver(GetChromeOptions());
                case AUTO_PARAMETER:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), _config.Webdriver.DriverVersion);
                    return new ChromeDriver(GetChromeOptions());
                default:
                    throw new Exception($"Invalid browser value: {type}");
            }
        }

        private EdgeDriver setupEdge()
        {
            var type = _config.Webdriver.Type;
            switch (_config.Webdriver.Type)
            {
                case CI_PARAMETER:
                    return new EdgeDriver(BINARY_FOLDER, GetEdgeOptions());
                case MANUAL_PARAMETER:
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeDriverConfig(_config), _config.Webdriver.DriverVersion);
                    return new EdgeDriver(GetEdgeOptions());
                case AUTO_PARAMETER:
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver(GetEdgeOptions());
                default:
                    throw new Exception($"Invalid browser value: {type}");
            }
        }

        private ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            if (_config.Webdriver.IsIncognito)
            {
                options.AddArgument("--incognito");
            }
            if (_config.Webdriver.IsHeadless)
            {
                options.AddArgument("--headless=new");
            }
            return options;
        }

        private EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions();
            // Configure Edge options here
            return options;
        }

        private void SetTimeouts()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_config.Timeouts.PageTimeoutInSeconds);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_config.Timeouts.ImplicitWaitTimeoutInSeconds);
            _driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(_config.Timeouts.AsynchronousJavaScriptTimeoutInSeconds);
        }

        private void RegisterListeners(EventFiringWebDriver driver)
        {
            DriverDebugListener listener = new DriverDebugListener(_config);
            driver.Navigated += listener.Navigating;
            driver.ElementClicked += listener.ElementClicked;
            driver.ElementClicking += listener.ElementClicking;
            driver.ElementValueChanging += listener.ElementValueChanging;
            driver.ElementValueChanged += listener.ElementValueChanged;
            driver.FindingElement += listener.FindingElement;
            driver.FindElementCompleted += listener.FindElementCompleted;
            driver.ScriptExecuted += listener.ScriptExecuted;
            driver.ExceptionThrown += listener.ExceptionThrown;
        }

    }
}
