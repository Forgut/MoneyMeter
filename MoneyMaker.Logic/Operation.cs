using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyMeter.Logic
{
    public class Operation
    {
        public decimal Value { get; set; }
        public ECategory Category { get; set; }
        public Operation(decimal value, ECategory category)
        {
            Value = value;
            Category = category;
        }
    }
}
