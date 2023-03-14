using System;
using System.Collections.Generic;
using System.Text;

namespace IntrusionDetection
{
    public class Operand : IMathExpression
    {
        public double Value { get; set; }
        public string Symbol { get; set; } = string.Empty;


        public override string ToString()
        {
            return Symbol;
        }
    }
}