using MicroappPlatformQaAutomation.Core.Config;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;

namespace MicroappPlatformQaAutomation.Core.Driver.WebdriverConfig
{
    public class EdgeDriverConfig : IDriverConfig
    {
        private const string URL = "https://msedgedriver.azureedge.net/";
        private const string WINDOWS = "windows";
        private const string LINUX = "linux";
        private TestConfiguration _testConfiguration;

        public EdgeDriverConfig(TestConfiguration configuration)
        {
            _testConfiguration = configuration;
        }

        public virtual string GetName()
        {
            return "Edge";
        }

        public virtual string GetUrl32()
        {
            var os = _testConfiguration.Webdriver.OS;
            var version = _testConfiguration.Webdriver.DriverVersion;

            switch (os)
            {
                case WINDOWS:
                    return URL + version + "/edgedriver_win64.zip";
                case LINUX:
                    return URL + version + "/edgedriver_linux64.zip";
                default:
                    throw new Exception($"Invalid os {os} or edge version {version}");
            }
        }

        public virtual string GetUrl64()
        {
            return GetUrl32();
        }

        public virtual string GetBinaryName()
        {
            var os = _testConfiguration.Webdriver.OS;

            switch (os)
            {
                case WINDOWS:
                    return "msedgedriver.exe";
                case LINUX:
                    return "msedgedriver";
                default:
                    throw new Exception($"Invalid OS value: {os}");
            }
        }

        public virtual string GetLatestVersion()
        {
            return GetLatestVersion(URL + "LATEST_STABLE");
        }

        public virtual string GetLatestVersion(string url)
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return RegistryHelper.GetInstalledBrowserVersionLinux("microsoft-edge", "--version");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RegistryHelper.GetInstalledBrowserVersionWin("msedge.exe");
            }

            throw new PlatformNotSupportedException("Your operating system is not supported");
        }
    }
}
