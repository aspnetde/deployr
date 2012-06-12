using System.Diagnostics;
using System.IO;
using DeployR.AgilityPackExtensions.NodeSelection;
using DeployR.FileGeneration;
using DeployR.Web;
using HtmlAgilityPack;

namespace DeployR.Exporting
{
    internal class StaticFileExporter
    {
        private readonly HtmlDocument _htmlDocument;
        private readonly string _renderedHtml;
        private ExportSettings _settings;

        public StaticFileExporter(HtmlDocument htmlDocument, string renderedHtml)
        {
            _htmlDocument = htmlDocument;
            _renderedHtml = renderedHtml;
        }

        public void ExportFiles(ExportSettings settings)
        {
            _settings = settings;

            DeleteAndRecreateDirectories();

            ExportCssFiles();
            ExportJavaScriptFiles();
            ExportHtmlFile();
        }

        private void DeleteAndRecreateDirectories()
        {
            if (Directory.Exists(_settings.Android.TargetDirectory))
            {
                Directory.Delete(_settings.Android.TargetDirectory, recursive: true);
            }

            if (Directory.Exists(_settings.iOS.TargetDirectory))
            {
                Directory.Delete(_settings.iOS.TargetDirectory, recursive: true);
            }

            Directory.CreateDirectory(_settings.Android.TargetDirectory);
            Directory.CreateDirectory(_settings.iOS.TargetDirectory);
        }

        private void ExportHtmlFile()
        {
            string modifiedHtml = _htmlDocument.DocumentNode.OuterHtml;

            string htmlFilePathAndroid = Path.Combine(_settings.Android.TargetDirectory, FileNameSettings.HtmlFileName);
            string htmlFilePathiOS = Path.Combine(_settings.iOS.TargetDirectory, FileNameSettings.HtmlFileName);

            FileWriter.WriteFile(htmlFilePathAndroid, modifiedHtml);
            FileWriter.WriteFile(htmlFilePathiOS, modifiedHtml);
        }

        private void ExportCssFiles()
        {
            HtmlNodeCollection stylesheetNodes = StylesheetNodeSelector.SelectStylesheetNodes(_htmlDocument);

            for (int i = 0; i < stylesheetNodes.Count; i++)
            {
                HtmlNode node = stylesheetNodes[i];
                
                string href = node.Attributes["href"].Value;
                string requestUri = RequestUriGenerator.GenerateUri(href);
                string cssRules = FileContentRequestLoader.DownloadString(requestUri);
                string fileName = string.Format(FileNameSettings.StylesheetFileNamePattern, i + 1);

                string stylesheetfilePathAndroid = Path.Combine(_settings.Android.TargetDirectory, fileName);
                string stylesheetfilePathiOS = Path.Combine(_settings.iOS.TargetDirectory, fileName);

                FileWriter.WriteFile(stylesheetfilePathAndroid, cssRules);
                FileWriter.WriteFile(stylesheetfilePathiOS, cssRules);

                HtmlNode newNode = _htmlDocument.CreateElement("link");
                newNode.CopyFrom(node);
                newNode.Attributes["href"].Value = fileName;
                node.ParentNode.ReplaceChild(newNode, node);
            }
        }

        private void ExportJavaScriptFiles()
        {
            HtmlNodeCollection javaScriptNodes = JavaScriptNodeSelector.SelectJavaScriptNodes(_htmlDocument);

            for (int i = 0; i < javaScriptNodes.Count; i++)
            {
                HtmlNode node = javaScriptNodes[i];

                string src = node.Attributes["src"].Value;
                string requestUri = RequestUriGenerator.GenerateUri(src);

                Trace.TraceWarning("JS: " + requestUri);

                string javaScriptFileContent = FileContentRequestLoader.DownloadString(requestUri);
                string fileName = string.Format(FileNameSettings.JavaScriptFileNamePattern, i + 1);

                string javaScriptFilePathAndroid = Path.Combine(_settings.Android.TargetDirectory, fileName);
                string javaScriptFilePathiOS = Path.Combine(_settings.iOS.TargetDirectory, fileName);

                FileWriter.WriteFile(javaScriptFilePathAndroid, javaScriptFileContent);
                FileWriter.WriteFile(javaScriptFilePathiOS, javaScriptFileContent);

                HtmlNode newNode = _htmlDocument.CreateElement("script");
                newNode.CopyFrom(node);
                newNode.Attributes["src"].Value = fileName;
                node.ParentNode.ReplaceChild(newNode, node);
            }
        }
    }
}
