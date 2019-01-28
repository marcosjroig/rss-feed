using Rss.Feed.Api.Helpers;
using Rss.Feed.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Rss.Feed.Api.Services
{
    public class FeedService : IFeedService
    {
        private readonly IHttpDownload _httpDownload;
        private readonly IDateParser _dateParser;
        public FeedService(IHttpDownload httpDownload, IDateParser dateParser)
        {
            _httpDownload = httpDownload;
            _dateParser = dateParser;
        }

        public IEnumerable<FeedItem> GetAllNews(string url)
        {
            try
            {
                //Get the content from the url
                var urlContent = _httpDownload.DownloadPage(url);

                //extract feed items
                var doc = XDocument.Parse(urlContent.Result);

                if (doc.Root != null)
                {
                    var feedItems = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                        select new FeedItem
                        {
                            Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                            Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                            PublishDate = _dateParser.ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                            Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                        };

                    return feedItems;
                }

                return new List<FeedItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<FeedItem>();
            }
        }
    }
}
