using Ensek.Library;
using Ensek.ViewModels;

namespace Ensek.Tests
{
    public class FileDuplicateHandlerTests
    {
        private readonly FileDuplicateHandler _fileDuplicateHandler = new FileDuplicateHandler();

        [Test]
        public void MeterReading_IsADuplicate_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                },
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("21/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                }
            };

            _fileDuplicateHandler.Process(meterReadings);

            var meterReading = meterReadings.Where(x => x.Error != null &&
                x.MeterReadingDateTime == DateTime.Parse("21/04/2019 09:24:00")).First();

            Assert.That(meterReading.Error, Is.EqualTo("This reading is a duplicate of another reading in this file"));
        }

        [Test]
        public void MeterReading_IsMultipleDuplicates_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                },
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("21/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                },
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("20/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                }
            };

            _fileDuplicateHandler.Process(meterReadings);

            var countInvalidMeterReadings = meterReadings.Where(x => x.Error != null &&
                x.MeterReadingDateTime != DateTime.Parse("22/04/2019 09:24:00")).Count();

            Assert.That(countInvalidMeterReadings, Is.EqualTo(2));
        }

        [Test]
        public void MeterReading_AreNoDuplicates_ReturnsNullErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 9999,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                },
                new MeterReadingViewModel {
                    AccountId = 9998,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                },
                new MeterReadingViewModel {
                    AccountId = 9997,
                    MeterReadingDateTime = DateTime.Parse("22/04/2019 09:24:00"),
                    MeterReadingValue = "000001"
                }
            };

            _fileDuplicateHandler.Process(meterReadings);

            var countInvalidMeterReadings = meterReadings.Where(x => x.Error != null).Count();

            Assert.That(countInvalidMeterReadings, Is.EqualTo(0));
        }
    }
}
