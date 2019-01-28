using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Rss.Feed.Api.Test.FakeHelpers
{
    public class FakeXml
    {
        private readonly string _xmlPath;
        public FakeXml()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testappconfig.json")
                .Build();
            _xmlPath = config["FakeXml:Path"];
        }

        public string ReadFakeXml()
        {
            var text = File.ReadAllText(_xmlPath, Encoding.UTF8);
            return text;
        }
    }
}
