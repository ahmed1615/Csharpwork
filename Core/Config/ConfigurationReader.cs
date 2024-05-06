using MicroappPlatformQaAutomation.Core.Commons;
using NUnit.Framework;

namespace MicroappPlatformQaAutomation.Core.Config
{
    [TestFixture]
    internal class ConfigurationReader
    {
        public const string BROWSER_PARAMETER = "browser";
        public const string HEADLESS_PARAMETER = "headless";
        public const string INCOGNITO_PARAMETER = "incognito";
        public const string HIGHLIGHT_ELEMENT_PARAMETER = "highlightElement";
        public const string TYPE_PARAMETER = "type";
        public const string DRIVER_VERSION_PARAMETER = "driverVersion";
        public const string OS_PARAMETER = "os";
        public const string PAGE_TIMEOUT_PARAMETER = "pageTimeout";
        public const string IMPLICIT_TIMEOUT_PARAMETER = "implicitTimeout";
        public const string SCRIPT_TIMEOUT_PARAMETER = "scriptTimeout";
        public const string URL_PARAMETER = "url";
        public const string URLDEMO_PARAMETER = "urldemo";

        public const string yamlConfigurationFilePath = "Resources/Configuration/environment.yaml";

        public static TestConfiguration GetTestConfiguration()
        {
            var config = YamlReader.FetchYamlData<TestConfiguration>(yamlConfigurationFilePath);
            ParseCommandLineArguments(config);
            return config;
        }

        [SetUp]
        private static void ParseCommandLineArguments(TestConfiguration configuration)
        {
            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(BROWSER_PARAMETER)))
            {
                configuration.Webdriver.Browser = TestContext.Parameters.Get(BROWSER_PARAMETER);
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(HEADLESS_PARAMETER)))
            {
                configuration.Webdriver.IsHeadless = bool.Parse(TestContext.Parameters.Get(HEADLESS_PARAMETER));
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(INCOGNITO_PARAMETER)))
            {
                configuration.Webdriver.IsIncognito = bool.Parse(TestContext.Parameters.Get(INCOGNITO_PARAMETER));
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(HIGHLIGHT_ELEMENT_PARAMETER)))
            {
                configuration.Webdriver.IsElementHighlightActive = bool.Parse(TestContext.Parameters.Get(HIGHLIGHT_ELEMENT_PARAMETER));
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(PAGE_TIMEOUT_PARAMETER)))
            {
                configuration.Timeouts.PageTimeoutInSeconds = int.Parse(TestContext.Parameters.Get(PAGE_TIMEOUT_PARAMETER));
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(IMPLICIT_TIMEOUT_PARAMETER)))
            {
                configuration.Timeouts.ImplicitWaitTimeoutInSeconds = int.Parse(TestContext.Parameters.Get(IMPLICIT_TIMEOUT_PARAMETER));
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(SCRIPT_TIMEOUT_PARAMETER)))
            {
                configuration.Timeouts.AsynchronousJavaScriptTimeoutInSeconds = int.Parse(TestContext.Parameters.Get(SCRIPT_TIMEOUT_PARAMETER));
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(URL_PARAMETER)))
            {
                configuration.Environment.URL = TestContext.Parameters.Get(URL_PARAMETER);
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(TYPE_PARAMETER)))
            {
                configuration.Webdriver.Type = TestContext.Parameters.Get(TYPE_PARAMETER);
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(DRIVER_VERSION_PARAMETER)))
            {
                configuration.Webdriver.DriverVersion = TestContext.Parameters.Get(DRIVER_VERSION_PARAMETER);
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get(OS_PARAMETER)))
            {
                configuration.Webdriver.OS = TestContext.Parameters.Get(OS_PARAMETER);
            }
        }
    }
}