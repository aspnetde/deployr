using System.Net;
using System.Text;

namespace DeployR.Web
{
    internal static class FileContentRequestLoader
    {
        public static string DownloadString(string requestUri)
        {
            WebClient webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            webClient.Headers.Add("user-agent", "69° DeployR");

            return webClient.DownloadString(requestUri);
        }
    }
}
