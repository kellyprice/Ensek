using AutoMapper;
using DAL;
using Ensek.ViewModels;
using System.Linq;

namespace Ensek.Data
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly IRepository<MeterReadingDTO> _meterReadings;
        private readonly IMapper _mapper;

        public MeterReadingRepository(IRepository<MeterReadingDTO> meterReadings, IMapper mapper)
        {
            _meterReadings = meterReadings;
            _mapper = mapper;
        }

        public IDictionary<int, DateTime> LatestMeterReadings(IEnumerable<int> accountIds)
        {
            try
            {
                return _meterReadings
                    .Get()
                    .Where(x => accountIds.Contains(x.AccountId))
                    .GroupBy(x => x.AccountId)
                    .Select(x => new
                    {
                        AccountId = x.Key,
                        MeterReadingDateTime = x.Max(x => x.MeterReadingDateTime),
                    })
                    .AsEnumerable()
                    .ToDictionary(key => key.AccountId, value => value.MeterReadingDateTime);
            }
            catch
            {
                // log error

                throw;
            }
        }

        public IEnumerable<MeterReadingDTO> PossibleDuplicates(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            try
            {
                var minDate = meterReadings.Min(x => x.MeterReadingDateTime);
                var accountIds = meterReadings.Select(x => x.AccountId);
                var meterReadingValues = meterReadings.Select(x => x.MeterReadingValue);

                return _meterReadings.Get()
                    .Where(x => x.MeterReadingDateTime >= minDate &&
                        accountIds.Contains(x.AccountId) &&
                        meterReadingValues.Contains(x.MeterReadingValue));
            }
            catch
            {
                // log error

                throw;
            }
        }

        public void Create(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            try
            {
                var meterReadingDtos = _mapper
                    .Map<IEnumerable<MeterReadingViewModel>, IEnumerable<MeterReadingDTO>>(meterReadings);

                _meterReadings.Create(meterReadingDtos);
            }
            catch
            {
                //log error

                throw;
            }
        }
    }
}
