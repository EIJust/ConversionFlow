using ConversionFlow.Core.Graph.Layer;
using System.Collections.Generic;
using System.Linq;

namespace ConversionFlow.Core.Link
{
    public abstract class Linker : ILinker
    {
        protected List<LinkInfo> _links;

        public IEnumerable<LinkInfo> Links
        {
            get => _links;
            private set => _links = value?.ToList();
        }

        protected Linker()
        {
            Links = new List<LinkInfo>();
        }

        public abstract void Link(BaseLayer source, BaseLayer target);
    }
}