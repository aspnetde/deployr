using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace DeployR.AgilityPackExtensions.NodeSelection
{
    internal class JavaScriptNodeSelector
    {

        private const string JavaScriptsXPath = "html//script[@type='text/javascript' and @src!='']";

        public static HtmlNodeCollection SelectJavaScriptNodes(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes(JavaScriptsXPath);
        }


    }
}
