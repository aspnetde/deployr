using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions.NodeSelection
{
    internal class JavaScriptNodeSelector
    {
        private const string CssStylesheetsXPath = "/html//script";

        public static HtmlNodeCollection SelectJavaScriptNodes(HtmlDocument htmlDocument)
        {
            HtmlNodeCollection scriptNodes = htmlDocument.DocumentNode.SelectNodes(CssStylesheetsXPath);
            RemoveNonJavaScriptNodesFromCollection(scriptNodes);

            return scriptNodes;
        }

        private static void RemoveNonJavaScriptNodesFromCollection(HtmlNodeCollection nodeCollection)
        {
            foreach (HtmlNode node in nodeCollection)
            {
                if (!node.IsJavaScript())
                {
                    nodeCollection.Remove(node);
                }
            }
        }
    }
}
