namespace MicroappPlatformQaAutomation.Core.Config.Parameters
{
    public class Webdriver
    {
        public string Browser { get; set; }

        public bool IsHeadless { get; set; }

        public bool IsIncognito { get; set; }

        public bool IsElementHighlightActive { get; set; }

        public string DriverVersion { get; set; }

        public string OS { get; set; }

        public string Type { get; set; }
    }
}
