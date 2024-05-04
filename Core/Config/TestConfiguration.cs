using MicroappPlatformQaAutomation.Core.Config.Parameters;

namespace MicroappPlatformQaAutomation.Core.Config
{
    public class TestConfiguration
    {
        public Env Environment { get; set; }

        public Timeouts Timeouts { get; set; }

        public Webdriver Webdriver { get; set; }
    }
}
