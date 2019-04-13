using ConversionFlow.Core.Conversion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversionFlow.App
{
    public class SqrConversion : IConversion
    {
        public IEnumerable<Type> RequiredEntryTypes { get; }
        public Type OutType { get; }

        public ConversionArgs Convert(ConversionArgs args)
        {
            var numArgs = args as NumericConversionArgs;
            var number = numArgs.Number;

            var sqr = number * number;

            return new NumericConversionArgs(sqr, this, args);
        }

        public Task<ConversionArgs> ConvertAsync(ConversionArgs args)
        {
            return Task.Run(() => Convert(args));
        }
    }
}