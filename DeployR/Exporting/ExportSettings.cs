using System;
using System.IO;

namespace DeployR.Exporting
{
    public class ExportSettings
    {
        public FileNameSettings Android { get; set; }
        public FileNameSettings iOS { get; set; }

        public ExportSettings()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string exportDirectory = Path.Combine(desktopPath, "Exporte für PhoneGap");

            string androidTargetDirectory = Path.Combine(exportDirectory, "Android");
            string iOSTargetDirectory = Path.Combine(exportDirectory, "iOS");

            Android = new FileNameSettings { TargetDirectory = androidTargetDirectory };
            iOS = new FileNameSettings { TargetDirectory = iOSTargetDirectory };
        }
    }
}