using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoneyMeter.Logic
{
    public class Balance
    {
        private string _filePath;
        private string[] GetOperationsFromFile()
        {
            var fileContent = File.ReadAllText(_filePath).Replace("\r", "").Replace("\n", "");
            return fileContent.ToLower().Split(';'); 
        }
        private void BuildListFromFile()
        {
            Operations = new List<Operation>();
            var operations = GetOperationsFromFile();
            foreach (var operation in operations)
            {
                var pair = operation.Split(':');
                if (pair.Length == 2)
                {
                    decimal.TryParse(pair[0], out decimal value);
                    ECategory category;
                    switch (pair[1])
                    {
                        case "income":
                            category = ECategory.Income;
                            break;
                        case "outcome":
                            category = ECategory.Outcome;
                            break;
                        default:
                            throw new Exception("Corrupted data in file");
                    }
                    Operations.Add(new Operation(value, category));
                }
            }
        }
        public Balance(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
                File.Create(_filePath);
            BuildListFromFile();
        }
        public void ClearFile()
        {
            File.WriteAllText(_filePath, string.Empty);
        }
        public void OverrideFileValues()
        {
            var newFileContent = string.Empty;
            foreach (var operation in Operations)
            {
                newFileContent += string.Concat(operation.Value, ":", operation.Category.ToString(), ";");
            }
            File.WriteAllText(_filePath, newFileContent);
        }
        public decimal SpentMoney
        {
            get
            {
                return Operations.Where(x => x.Category != ECategory.Income).Sum(x => x.Value);
            }
        }
        public decimal MaxValue
        {
            get
            {
                return Operations.Where(x => x.Category == ECategory.Income).Sum(x => x.Value);
            }
        }
        public decimal MoneyToSpare
        {
            get
            {
                return MaxValue - SpentMoney;
            }
        }
        public List<Operation> Operations { get; internal set; }
        public void AddDailyValue(decimal value)
        {
            Operations.Add(new Operation(value, ECategory.Income));
        }
        public void AddSpentMoney(decimal value)
        {
            Operations.Add(new Operation(value, ECategory.Outcome));
        }
    }
}
