using Ensek.Library;
using Ensek.ViewModels;

namespace Ensek.Tests
{
    public class MeterReadingHandlerTests
    {
        private readonly MeterReadingHandler _meterReadingHandler = new MeterReadingHandler();

        [Test]
        public void MeterReading_IsTooLong_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                }
            };

            _meterReadingHandler.Process(meterReadings);

            Assert.That(meterReadings.First().Error, Is.EqualTo("Invalid meter reading"));
        }

        [Test]
        public void MeterReading_IsAlphaValue_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "ABC"
                }
            };

            _meterReadingHandler.Process(meterReadings);

            Assert.That(meterReadings.First().Error, Is.EqualTo("Invalid meter reading"));
        }

        [Test]
        public void MeterReading_IsANegativeNumber_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "-100"
                }
            };

            _meterReadingHandler.Process(meterReadings);

            Assert.That(meterReadings.First().Error, Is.EqualTo("Invalid meter reading"));
        }

        [Test]
        public void MeterReading_IsValidInput_ReturnsNullErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "99999"
                }
            };

            _meterReadingHandler.Process(meterReadings);

            Assert.That(meterReadings.First().Error, Is.Null);
        }
    }
}
