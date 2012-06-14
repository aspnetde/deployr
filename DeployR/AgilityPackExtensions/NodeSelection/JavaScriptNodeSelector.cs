using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions.NodeSelection
{
    internal static class JavaScriptNodeSelector
    {
        private const string JavaScriptsXPath = "html//script[@type='text/javascript' and @src!='']";

        public static HtmlNodeCollection SelectJavaScriptNodes(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes(JavaScriptsXPath);
        }
    }
}
