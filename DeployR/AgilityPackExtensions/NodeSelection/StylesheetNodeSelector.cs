using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions.NodeSelection
{
    internal static class StylesheetNodeSelector
    {
        private const string StylesheetLinksXPath = "/html/head/link[@rel='stylesheet' and @href!='']";

        public static HtmlNodeCollection SelectStylesheetNodes(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes(StylesheetLinksXPath);
        }
    }
}
