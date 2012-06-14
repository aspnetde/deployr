using System;
using System.IO;

namespace DeployR.Exporting
{
    public class ExportSettings
    {
        public string TargetDirectory { get; set; }
        public string HtmlFileName { get; set; }
        public string StylesheetFileNamePattern { get; set; }
        public string JavaScriptFileNamePattern { get; set; }
        public string ImageFileNamePattern { get; set; }

        public ExportSettings()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            HtmlFileName = "index.html";
            StylesheetFileNamePattern = "stylesheet-{0}.css";
            JavaScriptFileNamePattern = "script-{0}.js";
            ImageFileNamePattern = "image-{0}.{1}";

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            TargetDirectory = Path.Combine(desktopPath, "export");
        }
    }
}