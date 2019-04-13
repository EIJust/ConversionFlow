using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConversionFlow.Core.Conversion;

namespace ConversionFlow.App
{
    public class SqrtConversion : IConversion
    {
        public IEnumerable<Type> RequiredEntryTypes { get; }
        public Type OutType { get; }

        public ConversionArgs Convert(ConversionArgs args)
        {
            var numArgs = args as NumericConversionArgs;
            var number = numArgs.Number;

            var sqrt = Math.Sqrt(number);

            return new NumericConversionArgs(sqrt, this, args);
        }

        public Task<ConversionArgs> ConvertAsync(ConversionArgs args)
        {
            return Task.Run(() => Convert(args));
        }
    }
}