namespace MicroappPlatformQaAutomation.Core.Config
{
    public class CommandLineOptions
    {
        public string Browser { get; set; }

        public bool? IsHeadless { get; set; }

        public bool? IsIncognito { get; set; }

        public string OS { get; set; }

        public string DriverVersion { get; set; }

        public string Type { get; set; }

        public int? PageTimeoutInSeconds { get; set; }

        public int? ImplicitWaitTimeoutInSeconds { get; set; }

        public int? AsynchronousJavaScriptTimeoutInSeconds { get; set; }

        public string URL { get; set; }
        public string URLDEMO { get; set; }
    }
}
