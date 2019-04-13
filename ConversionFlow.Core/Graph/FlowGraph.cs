using System.Collections.Generic;
using System.Threading.Tasks;
using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph.Layer;

namespace ConversionFlow.Core.Graph
{
    public class FlowGraph : IFlowGraph
    {
        public IEnumerable<BaseLayer> Layers { get; protected set; }

        public IEnumerable<BaseLayer> FirstConversionLayers { get; protected set; }
        public IEnumerable<BaseLayer> FinalConversionLayers { get; protected set; }

        public FlowGraph(IEnumerable<BaseLayer> firstLayers, IEnumerable<BaseLayer> finalLayers, IEnumerable<BaseLayer> layers)
        {
            Layers = layers;
            FirstConversionLayers = firstLayers;
            FinalConversionLayers = finalLayers;
        }

        public virtual void Post(ConversionArgs input)
        {
            foreach (var layer in FirstConversionLayers)
            {
                layer.Post(input);
            }
        }

        public virtual Task PostAsync(ConversionArgs input)
        {
            return Task.Run(() =>
            {
                foreach (var layer in FirstConversionLayers)
                    layer.PostAsync(input);
            });
        }
    }
}