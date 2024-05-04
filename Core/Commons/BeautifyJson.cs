using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    [TestFixture]
    public class BeautifyJson
    {
        public string BeautifyResponse(RestResponse response)
        {
            string content = response.Content;
            if (response.ContentType.Contains("application/json"))
            {
                try
                {
                    JToken token = JToken.Parse(content);
                    content = token.ToString(Newtonsoft.Json.Formatting.Indented);
                }
                catch (JsonReaderException)
                {
                    return content;
                }
            }
            return content;
        }
    }
}