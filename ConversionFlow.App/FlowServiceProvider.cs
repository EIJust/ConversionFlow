using System;
using ConversionFlow.App;
using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph;
using ConversionFlow.Core.Service;

public class FlowServiceProvider : IFlowServiceProvider
{
    private readonly IFlowGraphBuilder _graphBuilder;
    private IFlowService _dataFlowService;

    public FlowServiceProvider(IFlowGraphBuilder graphBuilder)
    {
        _graphBuilder = graphBuilder;
    }

    public IFlowService Provide(Action<ConversionArgs> callback)
    {
        return _dataFlowService ?? (_dataFlowService = new NumericFlowService(_graphBuilder, callback));
    }
}