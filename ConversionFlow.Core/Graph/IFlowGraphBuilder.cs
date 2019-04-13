using System.Collections.Generic;
using ConversionFlow.Core.Graph.Layer;

namespace ConversionFlow.Core.Graph
{
    public interface IFlowGraphBuilder
    {
        IFlowGraphBuilder Add(BaseLayer layer);
        IFlowGraphBuilder Add(IEnumerable<BaseLayer> layer);

        IFlowGraphBuilder AddAndSetAsFinal(BaseLayer baseLayer);
        IFlowGraphBuilder AddAndSetAsFinal(IEnumerable<BaseLayer> layers);

        IFlowGraphBuilder Link(BaseLayer outputBaseLayer, BaseLayer entryBaseLayer);
        IFlowGraphBuilder Link(IEnumerable<BaseLayer> outputLayers, BaseLayer entryBaseLayer);
        IFlowGraphBuilder Link(IEnumerable<BaseLayer> outputLayers, IEnumerable<BaseLayer> entryLayers);
        IFlowGraphBuilder Link(BaseLayer outputBaseLayer, IEnumerable<BaseLayer> entryLayers);

        IFlowGraphBuilder AddAndLink(BaseLayer outputBaseLayer, BaseLayer entryBaseLayer);
        IFlowGraphBuilder AddAndLink(IEnumerable<BaseLayer> outputLayers, BaseLayer entryBaseLayer);
        IFlowGraphBuilder AddAndLink(IEnumerable<BaseLayer> outputLayers, IEnumerable<BaseLayer> entryLayers);
        IFlowGraphBuilder AddAndLink(BaseLayer outputBaseLayer, IEnumerable<BaseLayer> entryLayers);

        IFlowGraph Build();
    }
}
