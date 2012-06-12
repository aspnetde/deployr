using System;
using System.IO;
using System.Web.Mvc;

namespace DeployR.Exporting
{
    internal class ViewStringExporter
    {
        private readonly Controller _controller;
        private readonly string _viewName;
        private readonly string _layoutViewName;
        private readonly object _model;

        public ViewStringExporter(Controller controller, string viewName, string layoutViewName, object model)
        {
            _controller = controller;
            _viewName = viewName;
            _layoutViewName = layoutViewName;
            _model = model;
        }

        public string RenderViewToString()
        {
            _controller.ViewData.Model = _model;

            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines
                        .FindView(_controller.ControllerContext, _viewName, _layoutViewName);

                    ViewContext viewContext = new ViewContext(_controller.ControllerContext, viewResult.View,
                                                              _controller.ViewData, _controller.TempData, writer);

                    viewResult.View.Render(viewContext, writer);

                    return writer.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}