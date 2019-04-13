using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversionFlow.Core.Conversion
{
    public interface IConversion
    {
        //replace this two properties to meta-info(i guess should add attributes)
        IEnumerable<Type> RequiredEntryTypes { get; }
        Type OutType { get; }

        ConversionArgs Convert(ConversionArgs args);
        Task<ConversionArgs> ConvertAsync(ConversionArgs args);
    }
}
