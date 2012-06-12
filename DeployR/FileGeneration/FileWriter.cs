using System.IO;

namespace DeployR.FileGeneration
{
    internal static class FileWriter
    {
        public static void WriteFile(string filePath, string content)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(content);
            }
        }
    }
}
