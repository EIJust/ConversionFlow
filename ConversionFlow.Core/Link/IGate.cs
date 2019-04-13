using ConversionFlow.Core.Graph.Layer;

namespace ConversionFlow.Core.Link
{
    public interface IGate
    {
        BaseLayer Layer { get; }
    }
}