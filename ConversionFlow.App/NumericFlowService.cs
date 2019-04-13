using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph;
using ConversionFlow.Core.Graph.Layer;
using ConversionFlow.Core.Service;
using System;
using System.Threading.Tasks;

namespace ConversionFlow.App
{
    public class NumericFlowService : IFlowService
    {
        private readonly IFlowGraph _graph;

        public NumericFlowService(IFlowGraphBuilder graphBuilder, Action<ConversionArgs> callback)
        {
            _graph = ConstructGraph(graphBuilder, callback);
        }

        public void Post(ConversionArgs args)
        {
            _graph.Post(args);
        }

        public Task PostAsync(ConversionArgs args)
        {
            return _graph.PostAsync(args);
        }

        private IFlowGraph ConstructGraph(IFlowGraphBuilder graphBuilder, Action<ConversionArgs> callback)
        {
            var sqrConversion = new SqrConversion();
            var sqrtConversion = new SqrtConversion();

            var firstConversionLayer = new SequenceLayer(new[] { sqrConversion });
            var secondConversionLayer = new SequenceLayer(new[] { sqrConversion });
            var thirdConversionLayer = new SequenceLayer(new[] { sqrtConversion }, callback);

            return graphBuilder
                .Add(firstConversionLayer)
                .AddAndSetAsFinal(secondConversionLayer)
                .Link(firstConversionLayer, secondConversionLayer)
                .AddAndSetAsFinal(thirdConversionLayer)
                .Link(secondConversionLayer, thirdConversionLayer)
                .Build();
        }
    }
}
