using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConversionFlow.Core.Graph.Layer
{
    public class SequenceLayer : BaseLayer
    {
        private readonly BufferBlock<ConversionArgs> _inputGateBlock;
        private readonly ActionBlock<ConversionArgs> _notifyBlock;

        private TransformBlock<ConversionArgs, ConversionArgs> _lastSequenceBlock;
        private TransformBlock<ConversionArgs, ConversionArgs> _firstSequenceBlock;
        private List<TransformBlock<ConversionArgs, ConversionArgs>> _transformBlocks;
        private BroadcastBlock<ConversionArgs> _outputGateBlock;

        public IEnumerable<IConversion> InnerConversions { get; }

        public SequenceLayer(IEnumerable<IConversion> conversions, Action<ConversionArgs> callback = null) : base(callback)
        {
            _inputGateBlock = new BufferBlock<ConversionArgs>();
            _transformBlocks = new List<TransformBlock<ConversionArgs, ConversionArgs>>();
            _notifyBlock = new ActionBlock<ConversionArgs>(args => Converted?.BeginInvoke(this, args, null, null));

            InnerConversions = conversions as IConversion[] ?? conversions.ToArray();

            RegisterSequenceBlocks(InnerConversions);

            _outputGateBlock = new BroadcastBlock<ConversionArgs>(args => (ConversionArgs) args.Clone());

            _lastSequenceBlock.LinkTo(_outputGateBlock);
            _inputGateBlock.LinkTo(_firstSequenceBlock);
            _outputGateBlock.LinkTo(_notifyBlock);

            InitGates(new EntryGateBlock(this, _inputGateBlock), new OutGateBlock(this, _outputGateBlock));

            void RegisterSequenceBlocks(IEnumerable<IConversion> conversionCollection)
            {
                TransformBlock<ConversionArgs, ConversionArgs> prevTransformBlock = null;
                foreach (var conversion in conversionCollection)
                {
                    var newBlock = new TransformBlock<ConversionArgs, ConversionArgs>(args => conversion.Convert(args));
                    _transformBlocks.Add(newBlock);
                    if (prevTransformBlock == null)
                    {
                        prevTransformBlock = newBlock;
                        _firstSequenceBlock = newBlock;
                    }
                    else
                    {
                        prevTransformBlock.LinkTo(newBlock);
                        prevTransformBlock = newBlock;
                    }
                }

                _lastSequenceBlock = prevTransformBlock;
            }
        }

        public override bool Post(ConversionArgs args)
        {
            return _inputGateBlock.Post(args);
        }

        public override Task<bool> PostAsync(ConversionArgs args)
        {
            return _inputGateBlock.SendAsync(args);
        }

        protected sealed override void InitGates(IEntryGate entryGate, IOutGate outGate)
        {
            EntryGate = entryGate;
            OutGate = outGate;
        }
    }
}