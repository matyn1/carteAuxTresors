using System.IO;

namespace CarteAuxTresors.Helpers
{
    /// <summary>A class that helps to read/write a file.</summary>
    public static class FileHelper
    {
        /// <summary>FileInfo of the entry file.</summary>
        private static FileInfo fileEntryInfo;

        /// <summary>Parses the entry file.</summary>
        /// <param name="path">The path of the entry file.</param>
        public static string[] ParseFileEntry(string path)
        {
            if (!File.Exists(path))
                return null;

            fileEntryInfo = new FileInfo(path);
            return File.ReadAllLines(path);
        }

        /// <summary>Writes the results in a file in the same directory of the entry file.</summary>
        /// <param name="results"></param>
        public static void WriteFileOutput(string results)
        {
            var outputFile = Path.Combine(fileEntryInfo.DirectoryName, "Output.txt");

            File.WriteAllText(outputFile, results);
        }
    }
}
