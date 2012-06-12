namespace DeployR.Exporting
{
    public class FileNameSettings
    {
        public const string HtmlFileName = "index.html";
        public const string StylesheetFileNamePattern = "stylesheet-{0}.css";
        public const string JavaScriptFileNamePattern = "script-{0}.js";

        public string TargetDirectory { get; set; }
    }
}
