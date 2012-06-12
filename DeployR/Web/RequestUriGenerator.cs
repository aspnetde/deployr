using System.Web;

namespace DeployR.Web
{
    internal static class RequestUriGenerator
    {
        public static string GenerateUri(string pathAndQuery)
        {
            string authorityTrimmed = HttpContext.Current.Request.Url.Authority.Trim('/');
            string scheme = HttpContext.Current.Request.Url.Scheme;
            string url = string.Format("{0}://{1}", scheme, authorityTrimmed);
            string pathAndQueryTrimmed = pathAndQuery.Trim('/');

            return string.Format("{0}/{1}", url, pathAndQueryTrimmed);
        }
    }
}