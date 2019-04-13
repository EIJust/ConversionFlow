using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph.Layer;

namespace ConversionFlow.Core.Link
{
    public class OutGateBlock:IOutGate
    {
        public OutGateBlock(BaseLayer Layer, ISourceBlock<ConversionArgs> block)
        {
            Source = block;
            EntryLinks = new List<LinkInfo>();
        }

        public ISourceBlock<ConversionArgs> Source { get; private set; }
        public BaseLayer Layer { get; }
        public IList<LinkInfo> EntryLinks { get; }
    }
}