using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using DeployR.AgilityPackExtensions.NodeSelection;
using DeployR.FileGeneration;
using DeployR.Web;
using HtmlAgilityPack;

namespace DeployR.Exporting
{
    internal class StaticFileExporter
    {
        private readonly HtmlDocument _htmlDocument;
        private ExportSettings _settings;
        private readonly IDictionary<string, string> _copiedImagePaths;

        public StaticFileExporter(HtmlDocument htmlDocument)
        {
            _htmlDocument = htmlDocument;
            _copiedImagePaths = new Dictionary<string, string>();
        }

        public void ExportFiles(ExportSettings settings)
        {
            _settings = settings;

            DeleteAndRecreateDirectory();

            ExportCssFiles();
            ExportJavaScriptFiles();
            ExportImagesHtml();

            // The HTML file has to be exported last so that the HTML tags
            // can be modified by the JavaScript, CSS, and image exporters
            ExportHtmlFile();
        }

        private void DeleteAndRecreateDirectory()
        {
            string[] files = Directory.GetFiles(_settings.TargetDirectory);

            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        private void ExportHtmlFile()
        {
            string modifiedHtml = _htmlDocument.DocumentNode.OuterHtml;
            string htmlFilePath = Path.Combine(_settings.TargetDirectory, _settings.HtmlFileName);

            FileWriter.WriteFile(htmlFilePath, modifiedHtml);
        }

        private void ExportCssFiles()
        {
            HtmlNodeCollection stylesheetNodes = StylesheetNodeSelector.SelectStylesheetNodes(_htmlDocument);

            if (stylesheetNodes == null)
            {
                return;
            }

            for (int i = 0; i < stylesheetNodes.Count; i++)
            {
                HtmlNode node = stylesheetNodes[i];

                string href = node.Attributes["href"].Value;
                string requestUri = RequestUriGenerator.GenerateUri(href);
                string cssRules = FileContentRequestLoader.DownloadString(requestUri);
                string fileName = string.Format(_settings.StylesheetFileNamePattern, i + 1, Path.GetFileNameWithoutExtension(href));
                string stylesheetfilePath = Path.Combine(_settings.TargetDirectory, fileName);

                cssRules = ReplaceCssImageReferences(cssRules);
                FileWriter.WriteFile(stylesheetfilePath, cssRules);

                HtmlNode newNode = _htmlDocument.CreateElement("link");
                newNode.CopyFrom(node);
                newNode.Attributes["href"].Value = fileName;
                node.ParentNode.ReplaceChild(newNode, node);
            }
        }

        private string ReplaceCssImageReferences(string cssRules)
        {
            const string referencedImagesPattern = @"(?<=url\("")(?<path>(?:.+?)\.(?<extension>[a-z]+))(?=""\))";
            cssRules = Regex.Replace(cssRules, referencedImagesPattern, CopyImageAndUpdateReference);

            return cssRules;
        }

        private string CopyImageAndUpdateReference(Match match)
        {
            string filePath = match.Groups["path"].Value;

            if (_copiedImagePaths.Keys.Contains(filePath))
            {
                return _copiedImagePaths[filePath];
            }

            string fileExtension = match.Groups["extension"].Value;
            string newFileName = string.Format(_settings.ImageFileNamePattern, _copiedImagePaths.Count + 1, Path.GetFileNameWithoutExtension(filePath), fileExtension);
            string imageSourcePath = HttpContext.Current.Server.MapPath(filePath);
            string imageDestinationPath = Path.Combine(_settings.TargetDirectory, newFileName);

            _copiedImagePaths.Add(filePath, newFileName);
            File.Copy(imageSourcePath, imageDestinationPath, overwrite: true);

            return newFileName;
        }

        private void ExportJavaScriptFiles()
        {
            HtmlNodeCollection javaScriptNodes = JavaScriptNodeSelector.SelectJavaScriptNodes(_htmlDocument);

            if (javaScriptNodes == null)
            {
                return;
            }

            for (int i = 0; i < javaScriptNodes.Count; i++)
            {
                HtmlNode node = javaScriptNodes[i];

                string src = node.Attributes["src"].Value;
                string requestUri = RequestUriGenerator.GenerateUri(src);
                string javaScriptFileContent = FileContentRequestLoader.DownloadString(requestUri);
                string fileName = string.Format(_settings.JavaScriptFileNamePattern, i + 1, Path.GetFileNameWithoutExtension(src));
                string javaScriptFilePath = Path.Combine(_settings.TargetDirectory, fileName);

                FileWriter.WriteFile(javaScriptFilePath, javaScriptFileContent);

                HtmlNode newNode = _htmlDocument.CreateElement("script");
                newNode.CopyFrom(node);
                newNode.Attributes["src"].Value = fileName;
                node.ParentNode.ReplaceChild(newNode, node);
            }
        }

        private void ExportImagesHtml()
        {
            HtmlNodeCollection imageNodes = ImageNodeSelector.SelectImageNodes(_htmlDocument);

            if (imageNodes == null)
            {
                return;
            }

            foreach (HtmlNode node in imageNodes)
            {
                string src = node.Attributes["src"].Value;
                string newFileName;

                if (_copiedImagePaths.Keys.Contains(src))
                {
                    newFileName = _copiedImagePaths[src];
                }
                else
                {
                    string imageUri = Path.Combine(_settings.TargetDirectory, src);
                    string imageSourcePath = HttpContext.Current.Server.MapPath(imageUri);
                    string extension = Path.GetExtension(imageSourcePath).TrimStart('.');
                    newFileName = string.Format(_settings.ImageFileNamePattern, _copiedImagePaths.Count + 1, Path.GetFileNameWithoutExtension(src), extension);
                    string imageDestinationPath = Path.Combine(_settings.TargetDirectory, newFileName);

                    _copiedImagePaths.Add(src, newFileName);
                    File.Copy(imageSourcePath, imageDestinationPath, overwrite: true);
                }

                HtmlNode newNode = _htmlDocument.CreateElement("img");
                newNode.CopyFrom(node);
                newNode.Attributes["src"].Value = newFileName;
                node.ParentNode.ReplaceChild(newNode, node);
            }
        }
    }
}
