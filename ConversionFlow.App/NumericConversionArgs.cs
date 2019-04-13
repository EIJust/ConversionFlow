using ConversionFlow.Core.Conversion;
using System.Collections.Generic;

namespace ConversionFlow.App
{
    public class NumericConversionArgs : ConversionArgs
    {
        public NumericConversionArgs(double num, IConversion generatedBy = null, ConversionArgs generatedFrom = null) : base(generatedBy, generatedFrom)
        {
            Number = num;
        }

        public double Number { get; }

        public override object Clone()
        {
            return new NumericConversionArgs(Number, GeneratedBy, GeneratedFrom);
        }
    }
}