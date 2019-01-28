using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rss.Feed.Api.Services;

namespace Rss.Feed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RssFeedController : ControllerBase
    {
        private readonly IFeedService _feedService;
        private readonly string _url;
        public RssFeedController(IFeedService feedService, IConfiguration config)
        {
            _feedService = feedService;
            var configuration = config;
            _url = configuration["RssFeed:Url"];
        }

        // GET: api/RssFeed
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var news = _feedService.GetAllNews(_url);
                if (news == null || !news.Any())
                {
                    return NotFound("No news were found from this RSS feeds");
                }

                return Ok(news);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("No news were found from this RSS feeds");
            }
        }
    }
}
