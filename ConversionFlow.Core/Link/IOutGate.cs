using System.Collections.Generic;

namespace ConversionFlow.Core.Link
{
    public interface IOutGate:IGate
    {
        IList<LinkInfo> EntryLinks { get; }
    }
}