using System;
using System.Net;
using System.Net.Http;
using Moq;

namespace Rss.Feed.Api.Test.FakeHelpers
{
    public enum ResponseType
    {
        Html,
        Xml
    }

    public class FakeHttpClientFactory
    {
        private static ResponseType _responseType;
        private static FakeXml _fakeXml;
        public FakeHttpClientFactory()
        {
            _fakeXml = new FakeXml();
        }
        
        //Returns the HttpClientFactory Moq
        public static Mock<IHttpClientFactory> GetHttpClientFactoryMock(ResponseType responseType)
        {
            _responseType = responseType;
            var fakeHttpClient = GetMoq(out var httpClientFactoryMock);
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(fakeHttpClient);
            return httpClientFactoryMock;
        }

        //Returns the HttpClientFactory Moq with an Exception
        public static Mock<IHttpClientFactory> GetHttpClientFactoryWithExceptionMock()
        {
            GetMoq(out var httpClientFactoryMock);
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Throws(new Exception());
            return httpClientFactoryMock;
        }

        private static HttpClient GetMoq(out Mock<IHttpClientFactory> httpClientFactoryMock)
        {
            //Prepare the response with a fake Html
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(GetResponseType())
            });

            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            httpClientFactoryMock = new Mock<IHttpClientFactory>();
            return fakeHttpClient;
        }

        private static string GetResponseType()
        {
            if (_responseType == ResponseType.Html)
            {
                //return html
                return "<html><head>...</head><body>....</body></html>";
            }

            //return json
            return _fakeXml.ReadFakeXml();
        }
    }
}