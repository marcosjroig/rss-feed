using Moq;
using Rss.Feed.Api.Services;
using Rss.Feed.Api.Test.FakeHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Rss.Feed.Api.Helpers;
using Xunit;

namespace Rss.Feed.Api.Test.Test.Services
{
    public class FeedServiceTest
    {
        private readonly IFeedService _sut;
        private readonly string _url;
        private readonly Mock<IHttpDownload> _webDownloaderMock;
        public FeedServiceTest()
        {
            _webDownloaderMock = new Mock<IHttpDownload>();
            var fakeXml = new FakeXml();
            _url = "http://good.uri";
            _webDownloaderMock.Setup(x => x.DownloadPage(It.IsAny<string>())).Returns(Task.FromResult(fakeXml.ReadFakeXml()));
            _sut = new FeedService(_webDownloaderMock.Object, new DateParser());
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsTwoNews()
        {
            //Act
            var news = _sut.GetAllNews(_url);

            //Assert
            Assert.Equal(2, news.Count());
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsTheRightNews()
        {
            //Act
            var news = _sut.GetAllNews(_url);

            //Assert
            Assert.Equal(2, news.Count());
            Assert.Equal("https://www.bbc.co.uk/news/uk-47013914", news.ToList()[0].Link);
            Assert.Equal("https://www.bbc.co.uk/sport/football/47013474", news.ToList()[1].Link);
        }

        [Fact]
        public void GetAll_WithInvalidXml_ReturnsZeroFeedItems()
        {
            //Arrange
            _webDownloaderMock.Setup(x => x.DownloadPage(It.IsAny<string>())).Returns(Task.FromResult("Invalid RSS Xml"));

            //Act
            var news = _sut.GetAllNews(_url);

            //Assert
            Assert.NotNull(news);
            Assert.Empty(news);
        }

        [Fact]
        public void GetAll_WithErrorException_ReturnsZeroFeedItems()
        {
            //Arrange
            _webDownloaderMock.Setup(x => x.DownloadPage(It.IsAny<string>())).Throws(new Exception());

            //Act
            var news = _sut.GetAllNews(_url);

            //Assert
            Assert.NotNull(news);
            Assert.Empty(news);
        }
    }
}
