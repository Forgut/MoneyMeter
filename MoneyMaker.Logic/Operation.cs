using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMeter.Logic
{
    public class Operation
    {
        public decimal Value { get; set; }
        public ECategory Category { get; set; }
        public DateTime DateAdded { get; set; }
        public Operation(decimal value, ECategory category, DateTime dateAdded)
        {
            Value = value;
            Category = category;
            DateAdded = dateAdded;
        }
        public override string ToString()
        {
            return string.Concat(Value, ":", Category.ToString(), ":", DateAdded.ToShortDateString());
        }
    }
}
