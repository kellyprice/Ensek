using Ensek.ViewModels;

namespace Ensek.Library
{
    public interface IHandler
    {
        void SetNextHandler(IHandler handler);
        void Process(IEnumerable<MeterReadingViewModel> meterReadings);
    }
}
