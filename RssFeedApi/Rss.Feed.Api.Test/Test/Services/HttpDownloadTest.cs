using System.Threading.Tasks;
using Rss.Feed.Api.Services;
using Rss.Feed.Api.Test.FakeHelpers;
using Xunit;

namespace Rss.Feed.Api.Test.Test.Services
{
    public class HttpDownloadTest
    {
        private readonly IHttpDownload _httpDownload;
        private readonly string _url;
        public HttpDownloadTest()
        {
            var httpClientFactoryMock = FakeHttpClientFactory.GetHttpClientFactoryMock(ResponseType.Html);
            _httpDownload = new HttpDownload(httpClientFactoryMock.Object);
            _url = "http://good.uri";
        }

        [Fact]
        public async Task GetPageAsString_WhenCalled_ReturnsThePageLikeString()
        {
            //Act
            var pageContent = _httpDownload.DownloadPage(_url);
            await pageContent;

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(pageContent.Result));
        }

        [Fact]
        public async Task GetPageAsString_WhenCalled_ReturnsTheRightHtml()
        {
            //Act
            var pageContent = _httpDownload.DownloadPage(_url);
            await pageContent;

            //Assert
            Assert.Equal("<html><head>...</head><body>....</body></html>", pageContent.Result);
        }

        [Fact]
        public async Task GetPageAsString_WithErrorException_ReturnsEmptyString()
        {
            //Arrange
            var httpClientFactoryMock = FakeHttpClientFactory.GetHttpClientFactoryWithExceptionMock();
            var httpDownload = new HttpDownload(httpClientFactoryMock.Object);

            //Act
            var pageContent = httpDownload.DownloadPage(_url);
            await pageContent;

            //Assert
            Assert.True(string.IsNullOrWhiteSpace(pageContent.Result));
        }

        [Fact]
        public async Task GetPageAsString_WithWrongUrl_ReturnsEmptyString()
        {
            //Act
            var pageContent = _httpDownload.DownloadPage("This is an Invalid URL");
            await pageContent;

            //Assert
            Assert.True(string.IsNullOrWhiteSpace(pageContent.Result));
        }
    }
}
