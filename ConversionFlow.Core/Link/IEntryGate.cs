using System.Collections.Generic;

namespace ConversionFlow.Core.Link
{
    public interface IEntryGate : IGate
    {
        IEnumerable<LinkInfo> OutLinks { get; }
    }
}