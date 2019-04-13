using System.Collections.Generic;

namespace ConversionFlow.Core.Link
{
    public class LinkInfo
    {
        public LinkInfo(IEntryGate entryGate, IOutGate outGate)
        {
            EntryGate = entryGate;
            TargetGates = new List<IOutGate> { outGate };
        }

        public LinkInfo(IEntryGate entryGate, IEnumerable<IOutGate> targetGates)
        {
            EntryGate = entryGate;
            TargetGates = targetGates;
        }

        public IEntryGate EntryGate { get; protected set; }
        public IEnumerable<IOutGate> TargetGates { get; protected set; }
    }
}