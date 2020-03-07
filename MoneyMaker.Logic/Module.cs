using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMeter.Logic
{
    public abstract class Module
    {
        protected string _filePath;
        protected FileFactory _fileFactory;
        public Module(string filePath)
        {
            _filePath = filePath;
            _fileFactory = new FileFactory(_filePath);
            _fileFactory.CreateIfNotExists();
            BuildListFromFile();
        }
        protected abstract void BuildListFromFile();
        public void ClearFile()
        {
            _fileFactory.ClearFile();
        }
        public abstract void OverrideFileValues();
    }
}
