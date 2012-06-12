using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions
{
    internal static class HtmlNodeExtensions
    {
        public static bool IsStylesheet(this HtmlNode node)
        {
            return node.Attributes["rel"].Value == "stylesheet";
        }
        
        public static bool IsJavaScript(this HtmlNode node)
        {
            return node.Attributes["type"].Value == "text/javascript";
        }
    }
}
