using System.Threading.Tasks;

namespace Rss.Feed.Api.Services
{
    public interface IHttpDownload
    {
        Task<string> DownloadPage(string url);
    }
}
