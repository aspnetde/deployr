using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions.NodeSelection
{
    internal static class ImageNodeSelector
    {
        private const string ImagesXPath = "html//img";

        public static HtmlNodeCollection SelectImageNodes(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes(ImagesXPath);
        }
    }
}
