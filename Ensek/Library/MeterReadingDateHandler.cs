using Ensek.Data;
using Ensek.ViewModels;

namespace Ensek.Library
{
    public class MeterReadingDateHandler : BaseHandler
    {
        private readonly IMeterReadingRepository _meterReadingRepository;

        public MeterReadingDateHandler(IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingRepository = meterReadingRepository;
        }

        public override void Process(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            var accountIds = meterReadings.Where(x => x.Error == null).Select(x => x.AccountId);
            var latestMeterReadings = _meterReadingRepository.LatestMeterReadings(accountIds);

            foreach (var meterReading in meterReadings.Where(x => x.Error == null))
            {
                if (latestMeterReadings.Any(x => x.Key == meterReading.AccountId && x.Value > meterReading.MeterReadingDateTime))
                    meterReading.Error = "A newer meter reading exists in the database";
            }

            if (_nextHandler != null) _nextHandler.Process(meterReadings);
        }
    }
}
