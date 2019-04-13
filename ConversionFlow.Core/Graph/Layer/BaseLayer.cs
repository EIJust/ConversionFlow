using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Link;
using System;
using System.Threading.Tasks;

namespace ConversionFlow.Core.Graph.Layer
{
    public abstract class BaseLayer
    {
        public IOutGate OutGate { get; protected set; }
        public IEntryGate EntryGate { get; protected set; }

        public BaseLayer(Action<ConversionArgs> callback)
        {
            if (callback != null)
            {
                Converted += (sender, args) => callback(args);
            }
        }

        public abstract Task<bool> PostAsync(ConversionArgs args);
        public abstract bool Post(ConversionArgs args);

        protected abstract void InitGates(IEntryGate entryGate, IOutGate outGate);

        protected EventHandler<ConversionArgs> Converted;
    }
}