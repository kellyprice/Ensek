using Ensek.Data;
using Ensek.ViewModels;

namespace Ensek.Library
{
    public class DuplicateHandler : BaseHandler
    {
        private readonly IMeterReadingRepository _meterReadingRepository;

        public DuplicateHandler(IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingRepository = meterReadingRepository;
        }

        public override void Process(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            var possibleDuplicates = _meterReadingRepository.PossibleDuplicates(meterReadings.Where(x => x.Error == null));

            foreach (var meterReading in meterReadings.Where(x => x.Error == null))
            {
                if (possibleDuplicates.Any(x => x.AccountId == meterReading.AccountId
                    && x.MeterReadingDateTime == meterReading.MeterReadingDateTime && x.MeterReadingValue == meterReading.MeterReadingValue))
                    meterReading.Error = "Duplicate of another reading in the database";
            }

            if (_nextHandler != null) _nextHandler.Process(meterReadings);
        }
    }
}
