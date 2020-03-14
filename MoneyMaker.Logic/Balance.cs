using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MoneyMeter.Logic
{
    public class Balance : Module
    {
        protected override void BuildListFromFile()
        {
            Operations = new List<Operation>();
            var operations = _fileFactory.ReadNormalizedData().Split(';');
            foreach (var operation in operations)
            {
                var pair = operation.Split(':');
                if (pair.Length == 3)
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
                            throw new Exception("Corrupted data in file " + _filePath);
                    }
                    if (!DateTime.TryParse(pair[2], out DateTime dateAdded))
                        throw new Exception("Corrucpted date time in file " + _filePath);

                    Operations.Add(new Operation(value, category, dateAdded));
                }
            }
        }
        public Balance(string filePath) : base(filePath)
        {
        }

        public override void OverrideFileValues()
        {
            _fileFactory.OverrideFileWithValues(Operations);
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
            Operations.Add(new Operation(value, ECategory.Income, DateTime.Today));
        }
        public void AddSpentMoney(decimal value)
        {
            Operations.Add(new Operation(value, ECategory.Outcome, DateTime.Today));
        }
    }
}
