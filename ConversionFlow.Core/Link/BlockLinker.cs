using ConversionFlow.Core.Exceptions;
using ConversionFlow.Core.Graph.Layer;
using System.Threading.Tasks.Dataflow;

namespace ConversionFlow.Core.Link
{
    public class BlockLinker : Linker
    {
        public override void Link(BaseLayer source, BaseLayer target)
        {
            var outGate = source.OutGate;
            var entryGate = target.EntryGate;

            if (outGate is OutGateBlock outGateBlock && entryGate is EntryGateBlock entryGateBlock)
            {
                outGateBlock.Source.LinkTo(entryGateBlock.Target);
                //entryGateBlock.Source.LinkTo(outGateBlock.Target);
            }
            else
            {
                throw new UnsupportedGateTypeException();
            }

            _links.Add(new LinkInfo(entryGate, outGate));
        }
    }
}