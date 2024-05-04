using NUnit.Framework;
using System.IO;
using YamlDotNet.Serialization;

namespace MicroappPlatformQaAutomation.Core.Commons
{
    [TestFixture]
    internal class YamlReader
    {
        public static T FetchYamlData<T>(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("Test data file not found.", filepath);
            }
            var deserializer = new DeserializerBuilder().Build();
            var yamlContent = File.ReadAllText(filepath);
            var test = deserializer.Deserialize<T>(yamlContent);
            return test;
        }
    }
}