using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph.Layer;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace ConversionFlow.Core.Link
{
    public class EntryGateBlock : IEntryGate
    {
        private readonly List<LinkInfo> _outLinks;

        public EntryGateBlock(BaseLayer layer, ITargetBlock<ConversionArgs> target)
        {
            _outLinks = new List<LinkInfo>();

            Layer = layer;
            Target = target;
        }

        public BaseLayer Layer { get; }
        public IEnumerable<LinkInfo> OutLinks { get; }
        public ITargetBlock<ConversionArgs> Target { get; }
    }
}