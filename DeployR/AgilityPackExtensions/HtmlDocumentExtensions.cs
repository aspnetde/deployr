using DeployR.AgilityPackExtensions.NodeSelection;
using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions
{
    internal static class HtmlDocumentExtensions
    {
        public static HtmlNodeCollection GetStylesheetNodes(this HtmlDocument htmlDocument)
        {
            return StylesheetNodeSelector.SelectStylesheetNodes(htmlDocument);
        }

        public static HtmlNodeCollection GetJavaScriptNodes(this HtmlDocument htmlDocument)
        {
            return JavaScriptNodeSelector.SelectJavaScriptNodes(htmlDocument);
        }
    }
}
