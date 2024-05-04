using MicroappPlatformQaAutomation.Core.Config;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;

namespace MicroappPlatformQaAutomation.Core.Driver.WebdriverConfig
{
    public class ChromeDriverConfig : IDriverConfig
    {
        private const string URL = "https://chromedriver.storage.googleapis.com/";
        private const string WINDOWS = "windows";
        private const string LINUX = "linux";
        private TestConfiguration _testConfiguration;

        public ChromeDriverConfig(TestConfiguration configuration)
        {
            _testConfiguration = configuration;
        }

        public string GetName()
        {
            return "Chrome";
        }

        public string GetUrl32()
        {
            var os = _testConfiguration.Webdriver.OS;
            var version = _testConfiguration.Webdriver.DriverVersion;

            switch (os)
            {
                case WINDOWS:
                    return URL + version + "/chromedriver_win32.zip";
                case LINUX:
                    return URL + version + "/chromedriver_linux64.zip";
                default:
                    throw new Exception($"Invalid os {os} or chrome version {version}");
            }
        }

        public string GetUrl64()
        {
            return GetUrl32();
        }

        public string GetBinaryName()
        {
            var os = _testConfiguration.Webdriver.OS;

            switch (os)
            {
                case WINDOWS:
                    return "chromedriver.exe";
                case LINUX:
                    return "chromedriver";
                default:
                    throw new Exception($"Invalid OS value: {os}");
            }
        }

        public string GetLatestVersion()
        {
            return GetLatestVersion(URL + "LATEST_RELEASE");
        }

        private string GetLatestVersion(string url)
        {
            Uri uri = new Uri(url);
            WebRequest webRequest = WebRequest.Create(uri);
            using WebResponse webResponse = webRequest.GetResponse();
            using Stream stream = webResponse.GetResponseStream();
            if (stream == null)
            {
                throw new ArgumentNullException($"Can't get content from URL: {uri}");
            }

            using StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd().Trim();
        }

        public string GetMatchingBrowserVersion()
        {
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return RegistryHelper.GetInstalledBrowserVersionLinux("google-chrome", "--product-version", "chromium", "--version", "chromium-browser", "--version");
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return RegistryHelper.GetInstalledBrowserVersionWin("chrome.exe");
                }

                throw new PlatformNotSupportedException("Your operating system is not supported");
            }
        }
    }
}
