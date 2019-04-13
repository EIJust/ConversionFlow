using ConversionFlow.App;
using ConversionFlow.Core.Conversion;
using ConversionFlow.Core.Graph;
using ConversionFlow.Core.Link;
using System;

namespace Flow.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ILinker blockLinker = new BlockLinker();
            IFlowGraphBuilder flowGraphBuilder = new FlowGraphBuilder(blockLinker);

            FlowServiceProvider provider = new FlowServiceProvider(flowGraphBuilder);

            var service = provider.Provide(Callback);

            for (var i = 0; i < 10; i++)
            {
                service.Post(new NumericConversionArgs(3, null, null));
            }

            Console.ReadLine();
        }

        private static void Callback(ConversionArgs args)
        {
            //TODO: thinking about correct getting values from args
            var numArgs = args as NumericConversionArgs;
            var number = numArgs.Number;

            Console.WriteLine(number);
        }
    }
}
