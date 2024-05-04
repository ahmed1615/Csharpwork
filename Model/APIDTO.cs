namespace MicroappPlatformQaAutomation.Model
{
    internal class APIDTO
    {
        public Data data { get; set; }
        public Support support { get; set; }
        public class Data
        {
            public int id { get; set; }
            public string name { get; set; }
            public int year { get; set; }
            public string color { get; set; }
            public string pantone_value { get; set; }
        }
        public class Support
        {
            public string url { get; set; }
            public string text { get; set; }
        }
    }
}
