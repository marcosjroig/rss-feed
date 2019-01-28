using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Rss.Feed.Api.Controllers;
using Rss.Feed.Api.Models;
using Rss.Feed.Api.Services;
using Xunit;

namespace Rss.Feed.Api.Test.Test.Controllers
{
    public class RssFeedControllerTest
    {
        private readonly RssFeedController _controller;
        private readonly Mock<IConfiguration> _configuration;

        public RssFeedControllerTest()
        {
            var fakeNews = Enumerable.Repeat(new FeedItem(), 10);
            var moqFeedService = new Mock<IFeedService>();
            moqFeedService.Setup(x => x.GetAllNews(It.IsAny<string>())).Returns(fakeNews);
            _configuration = new Mock<IConfiguration>();
            _configuration.SetupAllProperties();
            _controller = new RssFeedController(moqFeedService.Object, _configuration.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Act
            var okResult = _controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsThenNews()
        {
            //Act
            var okResult = _controller.Get() as OkObjectResult;
            IEnumerable<FeedItem> news = new List<FeedItem>();

            //Assert
            if (okResult != null) news = Assert.IsAssignableFrom<IEnumerable<FeedItem>>(okResult.Value);
            Assert.Equal(10, news.Count());
        }

        [Fact]
        public void Get_WithEmptyUrl_ReturnsNotFound()
        {
            //Arrange
            var moqFeedService = new Mock<IFeedService>();
            moqFeedService.Setup(x => x.GetAllNews("")).Returns(new List<FeedItem>());
            var controller = new RssFeedController(moqFeedService.Object, _configuration.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Get_WithNullResult_ReturnsNotFound()
        {
            //Arrange
            var moqFeedService = new Mock<IFeedService>();
            moqFeedService.Setup(x => x.GetAllNews("")).Returns((List<FeedItem>) null);
            var controller = new RssFeedController(moqFeedService.Object, _configuration.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Get_WithErrorException_ReturnsNotFound()
        {
            //Arrange
            var moqFeedService = new Mock<IFeedService>();
            moqFeedService.Setup(x => x.GetAllNews(It.IsAny<string>())).Throws(new Exception());
            var controller = new RssFeedController(moqFeedService.Object, _configuration.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
