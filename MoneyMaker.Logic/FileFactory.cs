using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoneyMeter.Logic
{
    public class FileFactory
    {
        private string _filePath;
        public FileFactory(string filePath)
        {
            _filePath = filePath;
        }
        public void CreateIfNotExists()
        {
            if (!File.Exists(_filePath))
                this.CreateFile();
        }
        public void CreateFile()
        {
            File.Create(_filePath);
        }
        public void ClearFile()
        {
            if (!File.Exists(_filePath))
            {
                this.CreateFile();
                return;
            }
            File.WriteAllText(_filePath, string.Empty);
        }
        public string ReadNormalizedData()
        {
            return File.ReadAllText(_filePath).Replace("\r", "").Replace("\n", "").ToLower();
        }
        public void OverrideFileWithValues<T>(List<T> values)
        {
            var newFileContent = string.Empty;
            foreach (var value in values)
            {
                newFileContent += string.Concat(value.ToString(), ";");
            }
            File.WriteAllText(_filePath, newFileContent);
        }
    }
}
