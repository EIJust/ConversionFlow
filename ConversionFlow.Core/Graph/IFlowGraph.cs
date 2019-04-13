using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph.Layer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversionFlow.Core.Graph
{
    public interface IFlowGraph
    {
        IEnumerable<BaseLayer> Layers { get; }
        IEnumerable<BaseLayer> FinalConversionLayers { get; }
        IEnumerable<BaseLayer> FirstConversionLayers { get; }

        void Post(ConversionArgs input);
        Task PostAsync(ConversionArgs input);
    }
}
