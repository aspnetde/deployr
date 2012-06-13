using System.Net;

namespace DeployR.Web
{
    internal class FileContentRequestLoader
    {
        public static string DownloadString(string requestUri)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "69° DeployR");
            return webClient.DownloadString(requestUri);
        }
    }
}
