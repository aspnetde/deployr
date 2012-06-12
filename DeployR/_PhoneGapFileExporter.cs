//using System;
//using System.IO;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using DeployR.AgilityPackExtensions;
//using DeployR.Exporting;
//using HtmlAgilityPack;

//namespace DeployR
//{
//    public class _PhoneGapFileExporter
//    {
//        private const string OutputFileNameHtml = "index.html";
//        private const string OutputFileNameCss = "style.css";
//        private const string DirectoryName = "Exporte für PhoneGap";

//        private static void WriteHtmlFile(string renderedHtml)
//        {
//            string filePathHtml = GetFilePathHtml();

//            using (FileStream stream = new FileStream(filePathHtml, FileMode.Create))
//            using (StreamWriter writer = new StreamWriter(stream))
//            {
//                writer.Write(renderedHtml);
//            }
//        }

//        private static void WriteCssFile(string cssRules)
//        {
//            string filePathCss = GetFilePathCss();

//            using (FileStream stream = new FileStream(filePathCss, FileMode.Create))
//            using (StreamWriter writer = new StreamWriter(stream))
//            {
//                writer.Write(cssRules);
//            }
//        }

//        private static void MakeSureDirectoryExists()
//        {
//            string directoryPath = GetDiretoryPath();
//            Directory.CreateDirectory(directoryPath);
//        }

//        private static string GetFilePathHtml()
//        {
//            string directoryPath = GetDiretoryPath();

//            return Path.Combine(directoryPath, OutputFileNameHtml);
//        }

//        private static string GetFilePathCss()
//        {
//            string directoryPath = GetDiretoryPath();

//            return Path.Combine(directoryPath, OutputFileNameCss);
//        }

//        private static string GetDiretoryPath()
//        {
//            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

//            return Path.Combine(desktopPath, DirectoryName);
//        }

//        private static string ReadCssRulesFromStylesheet(string renderedHtml)
//        {
//            HtmlDocument htmlDocument = CreateDocumentFromHtml(renderedHtml);
//            HtmlNodeCollection stylesheetNodes = htmlDocument.GetStylesheetNodes();
//            HtmlNodeCollection javaScriptNodes = htmlDocument.GetJavaScriptNodes();

//            foreach (HtmlNode node in )
//            {
//                string applicationBasePath = HttpContext.Current.Request.Url.AbsoluteUri;
//                string cssHref = node.Attributes["href"].Value;
//                string cssRequestUri = CombineCssFilePathParts(applicationBasePath, cssHref);

//                WebClient webClient = new WebClient();
//                string cssRules = webClient.DownloadString(cssRequestUri);

//                return cssRules;
//            }

//            throw new InvalidOperationException("No CSS stylesheet found.");
//        }

//        private static string CombineCssFilePathParts(string applicationBasePath, string cssHref)
//        {
//            return applicationBasePath.Trim('/') + "/" + cssHref.Trim('/');
//        }
//    }
//}