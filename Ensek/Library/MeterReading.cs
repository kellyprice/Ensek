using Ensek.Data;
using Ensek.ViewModels;

namespace Ensek.Library
{
    public class MeterReading
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        private readonly Validate _validate;

        public MeterReading(IMeterReadingRepository meterReadingRepository, Validate validate)
        {
            _meterReadingRepository = meterReadingRepository;
            _validate = validate;
        }

        public IEnumerable<MeterReadingViewModel> Process(FileViewModel file)
        {
            var meterReadings = FileUtil.GetMeterReadings(file);

            _validate.Process(meterReadings);

            _meterReadingRepository.Create(meterReadings.Where(x => x.Error == null));

            return meterReadings;
        }
    }
}
