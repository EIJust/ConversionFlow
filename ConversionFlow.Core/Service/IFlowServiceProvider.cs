using ConversionFlow.Core.Conversion;
using System;

namespace ConversionFlow.Core.Service
{
    public interface IFlowServiceProvider
    {
        IFlowService Provide(Action<ConversionArgs> callback);
    }
}