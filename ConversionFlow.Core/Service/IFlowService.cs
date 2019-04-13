using System.Threading.Tasks;
using ConversionFlow.Core.Conversion;

namespace ConversionFlow.Core.Service
{
    public interface IFlowService
    {
        void Post(ConversionArgs args);
        Task PostAsync(ConversionArgs args);
    }
}