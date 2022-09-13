using Ensek.ViewModels;

namespace Ensek.Library
{
    public class MeterReadingHandler : BaseHandler
    {
        public override void Process(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            foreach (var meterReading in meterReadings.Where(x => x.Error == null))
            {
                _ = int.TryParse(meterReading.MeterReadingValue, out int meterReadingValue);

                if (meterReadingValue < 1 || meterReading.MeterReadingValue?.Length > 5)
                    meterReading.Error = "Invalid meter reading";
                else
                    meterReading.MeterReadingValue = $"{meterReadingValue:00000}";
            }

            if (_nextHandler != null) _nextHandler.Process(meterReadings);
        }
    }
}
