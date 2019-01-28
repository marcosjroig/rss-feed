using System;

namespace Rss.Feed.Api.Helpers
{
    public interface IDateParser
    {
        DateTime ParseDate(string date);
    }
}
