using System;
using System.Collections.Generic;
using System.Linq;
using ConversionFlow.Core.Graph.Layer;
using ConversionFlow.Core.Link;

namespace ConversionFlow.Core.Graph
{
    public class FlowGraphBuilder : IFlowGraphBuilder
    {
        private readonly List<BaseLayer> _layersList;
        private readonly ILinker _linker;

        private IEnumerable<BaseLayer> _firstLayers;
        private IEnumerable<BaseLayer> _finalLayers;

        public FlowGraphBuilder(ILinker linker)
        {
            _layersList = new List<BaseLayer>();

            _firstLayers = null;
            _finalLayers = null;

            _linker = linker;
        }

        public IFlowGraphBuilder Add(BaseLayer layer)
        {
            if (_layersList.Count == 0)
            {
                _firstLayers = new[] { layer };
            }
            _layersList.Add(layer);
            
            return this;
        }

        public IFlowGraphBuilder Add(IEnumerable<BaseLayer> layers)
        {
            var conversionLayers = layers.ToList();

            if (_layersList.Count == 0)
            {
                _firstLayers = conversionLayers;
            }
            _layersList.AddRange(conversionLayers);
            
            return this;
        }

        public IFlowGraphBuilder AddAndSetAsFinal(BaseLayer baseLayer)
        {
            _layersList.Add(baseLayer);
            _finalLayers = new List<BaseLayer> { baseLayer };

            return this;
        }

        public IFlowGraphBuilder AddAndSetAsFinal(IEnumerable<BaseLayer> layers)
        {
            var conversionLayers = layers.ToList();

            _layersList.AddRange(conversionLayers);
            _finalLayers = conversionLayers;

            return this;
        }

        public IFlowGraphBuilder AddAndLink(BaseLayer outputBaseLayer, BaseLayer entryBaseLayer)
        {
            return
                Add(outputBaseLayer)
                .Add(entryBaseLayer)
                .Link(entryBaseLayer, outputBaseLayer);
        }

        public IFlowGraphBuilder AddAndLink(IEnumerable<BaseLayer> outputLayers, BaseLayer entryBaseLayer)
        {
            var incomingLayersArray = outputLayers as BaseLayer[] ?? outputLayers.ToArray();

            return
                Add(entryBaseLayer)
                .Add(incomingLayersArray)
                .Link(incomingLayersArray, entryBaseLayer);
        }

        public IFlowGraphBuilder AddAndLink(IEnumerable<BaseLayer> outputLayers, IEnumerable<BaseLayer> entryLayers)
        {
            var outcomingLayersArray = entryLayers as BaseLayer[] ?? entryLayers.ToArray();
            var incomingLayersArray = outputLayers as BaseLayer[] ?? outputLayers.ToArray();

            return
                Add(incomingLayersArray)
                .Add(outcomingLayersArray)
                .Link(incomingLayersArray, outcomingLayersArray);
        }

        public IFlowGraphBuilder AddAndLink(BaseLayer outputBaseLayer, IEnumerable<BaseLayer> entryLayers)
        {
            var entryLayersArray = entryLayers as BaseLayer[] ?? entryLayers.ToArray();

            return
                Add(outputBaseLayer)
                .Add(entryLayersArray)
                .Link(outputBaseLayer, entryLayersArray);
        }

        public IFlowGraphBuilder Link(BaseLayer outputBaseLayer, BaseLayer entryBaseLayer)
        {
            _linker.Link(outputBaseLayer, entryBaseLayer);

            return this;
        }

        public IFlowGraphBuilder Link(IEnumerable<BaseLayer> outputLayers, BaseLayer entryBaseLayer)
        {
            foreach (var incomingLayer in outputLayers)
            {
                _linker.Link(incomingLayer, entryBaseLayer);
            }

            return this;
        }

        public IFlowGraphBuilder Link(IEnumerable<BaseLayer> outputLayers, IEnumerable<BaseLayer> entryLayers)
        {
            var conversionLayers = entryLayers as BaseLayer[] ?? entryLayers.ToArray();

            foreach (var incomingLayer in outputLayers)
            {
                foreach (var layer in conversionLayers)
                {
                    _linker.Link(incomingLayer, layer);
                }
            }

            return this;
        }

        public IFlowGraphBuilder Link(BaseLayer outputBaseLayer, IEnumerable<BaseLayer> entryLayers)
        {
            var conversionLayers = entryLayers as BaseLayer[] ?? entryLayers.ToArray();

            foreach (var layer in conversionLayers)
            {
                _linker.Link(outputBaseLayer, layer);
            }

            return this;
        }

        public IFlowGraph Build()
        {
            if (_firstLayers == null)
            {
                throw new InvalidOperationException("Not initialized first layers");
            }

            if (_finalLayers == null)
            {
                throw new InvalidOperationException("Not initialized final layers");
            }

            return new FlowGraph(_firstLayers, _finalLayers, _layersList);
        }
    }
}