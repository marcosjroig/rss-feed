using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rss.Feed.Api.Services
{
    public class HttpDownload : IHttpDownload
    {
        private readonly IHttpClientFactory _httpFactory;
        public HttpDownload(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<string> DownloadPage(string url)
        {
            try
            {
                // Use HttpClient.
                using (var client = _httpFactory.CreateClient())
                using (var response = await client.GetAsync(url))

                    // If the response is OK
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var content = response.Content)
                        {
                            // Read the string.
                            var result = await content.ReadAsStringAsync();

                            // Return the result.
                            return result;
                        }
                    }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}
