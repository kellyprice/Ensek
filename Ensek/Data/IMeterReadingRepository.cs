using DAL;
using Ensek.ViewModels;

namespace Ensek.Data
{
    public interface IMeterReadingRepository
    {
        IDictionary<int, DateTime> LatestMeterReadings(IEnumerable<int> accountIds);
        IEnumerable<MeterReadingDTO> PossibleDuplicates(IEnumerable<MeterReadingViewModel> meterReadings);
        void Create(IEnumerable<MeterReadingViewModel> meterReadings);
    }
}
