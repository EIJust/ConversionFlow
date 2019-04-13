using System.Collections.Generic;
using ConversionFlow.Core.Graph.Layer;

namespace ConversionFlow.Core.Link
{
    public interface ILinker
    {
        IEnumerable<LinkInfo> Links { get; }

        void Link(BaseLayer source, BaseLayer target);
    }
}