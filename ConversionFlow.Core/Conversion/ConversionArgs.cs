using System;
using System.Collections.Generic;
using System.Linq;

namespace ConversionFlow.Core.Conversion
{
    public abstract class ConversionArgs : Dictionary<Type, object>, ICloneable
    {
        protected ConversionArgs _generatedFrom;

        public ConversionArgs(IConversion generatedBy, ConversionArgs generatedFrom)
        {
            GeneratedFrom = generatedFrom;
            GeneratedBy = generatedBy;
        }

        public IConversion GeneratedBy { get; protected set; }

        public ConversionArgs GeneratedFrom
        {
            get => _generatedFrom;
            private set => _generatedFrom = value;
        }

        public T GetArgs<T>()
        {
            return (T)this[typeof(T)];
        }

        public abstract object Clone();
    }
}