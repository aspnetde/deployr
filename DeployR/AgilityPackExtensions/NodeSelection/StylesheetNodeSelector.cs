using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions.NodeSelection
{
    internal class StylesheetNodeSelector
    {
        private const string StylesheetLinksXPath = "/html/head/link";

        public static HtmlNodeCollection SelectStylesheetNodes(HtmlDocument htmlDocument)
        {
            HtmlNodeCollection linkNodes = htmlDocument.DocumentNode.SelectNodes(StylesheetLinksXPath);
            RemoveNonCssNodesFromCollection(linkNodes);

            return linkNodes;
        }

        private static void RemoveNonCssNodesFromCollection(HtmlNodeCollection nodeCollection)
        {
            foreach (HtmlNode node in nodeCollection.Nodes())
            {
                if (!node.IsJavaScript())
                {
                    nodeCollection.Remove(node);
                }
            }
        }
    }
}
