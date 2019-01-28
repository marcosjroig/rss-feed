using System;
using static System.DateTime;

namespace Rss.Feed.Api.Helpers
{
    public class DateParser: IDateParser
    {
        public DateTime ParseDate(string date)
        {
            return TryParse(date, out var result) ? result : MinValue;
        }
    }
}
