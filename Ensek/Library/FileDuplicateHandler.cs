using Ensek.ViewModels;

namespace Ensek.Library
{
    public class FileDuplicateHandler : BaseHandler
    {
        public override void Process(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            var duplicates = meterReadings
                .Where(x => x.Error == null)
                .GroupBy(x => x.AccountId)
                .Select(y => new
                {
                    AccountId = y.Key,
                    MeterReadingDateTime = y.Max(x => x.MeterReadingDateTime),
                    Count = y.Count()
                })
                .Where(x => x.Count > 1);

            foreach (var meterReading in meterReadings.Where(x => x.Error == null))
            {
                if (duplicates.Any(x => x.AccountId == meterReading.AccountId && x.MeterReadingDateTime > meterReading.MeterReadingDateTime))
                    meterReading.Error = "Duplicate of another reading in this file";
            }

            if (_nextHandler != null) _nextHandler.Process(meterReadings);
        }
    }
}
