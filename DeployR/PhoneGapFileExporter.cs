using System.Web.Mvc;
using DeployR.Exporting;
using HtmlAgilityPack;

namespace DeployR
{
    public class PhoneGapFileExporter
    {
        private readonly Controller _controller;
        private readonly string _viewName;
        private readonly string _layoutViewName;
        private readonly object _model;
        
        private string _renderedHtml;
        private HtmlDocument _htmlDocument;

        public PhoneGapFileExporter(Controller controller, string viewName, string layoutViewName, object model)
        {
            _controller = controller;
            _viewName = viewName;
            _layoutViewName = layoutViewName;
            _model = model;
        }

        public void ExportHtmlCssJavaScriptFiles()
        {
            ExportSettings settings = new ExportSettings();
            ExportHtmlCssJavaScriptFiles(settings);
        }

        public void ExportHtmlCssJavaScriptFiles(ExportSettings settings)
        {
            RenderHtmlFromView();
            CreateHtmlDocumentFromHtml();

            StaticFileExporter fileExporter = new StaticFileExporter(_htmlDocument);
            fileExporter.ExportFiles(settings);
        }

        private void RenderHtmlFromView()
        {
            ViewStringExporter exporter = new ViewStringExporter(_controller, _viewName, _layoutViewName, _model);
            _renderedHtml = exporter.RenderViewToString();
        }

        private void CreateHtmlDocumentFromHtml()
        {
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(_renderedHtml);
        }
    }
}
