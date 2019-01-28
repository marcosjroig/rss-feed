using System.Collections.Generic;
using Rss.Feed.Api.Models;

namespace Rss.Feed.Api.Services
{
    public interface IFeedService
    {
        IEnumerable<FeedItem> GetAllNews(string url);
    }
}
