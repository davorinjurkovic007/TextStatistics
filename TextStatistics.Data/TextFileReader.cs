using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace TextStatistics.Data
{
    public class TextFileReader : ITextFileReader
    {
        private const string FOLDER_NAME = "Data";
        private string filePath = string.Empty;

        public string TextOutput { get; private set; }

        public TextFileReader()
        {
        }

        public void ReadTextFile(string nameOfFile)
        {
            StringBuilder textBuilder = new StringBuilder();

            try
            {
                FilePath(nameOfFile);

                using (StreamReader streamReader = File.OpenText(filePath))
                {
                    string stream = String.Empty;
                    while ((stream = streamReader.ReadLine()) != null)
                    {
                        textBuilder.AppendLine(stream);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            TextOutput = textBuilder.ToString();
        }

        private void FilePath(string fileName)
        {
            if (!File.Exists(fileName))
            {
                filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FOLDER_NAME, Path.GetFileName(fileName));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find the specified file.", fileName);
            }
        }
    }
}
