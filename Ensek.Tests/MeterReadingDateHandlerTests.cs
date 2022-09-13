using Ensek.Data;
using Ensek.Library;
using Ensek.ViewModels;
using Moq;

namespace Ensek.Tests
{
    public class MeterReadingDateHandlerTests
    {
        private readonly MeterReadingDateHandler _meterReadingDateHandler;

        public MeterReadingDateHandlerTests()
        {
            var mockMeterReadings = new Mock<IMeterReadingRepository>();

            mockMeterReadings.Setup(m => m.LatestMeterReadings(It.IsAny<IEnumerable<int>>())).Returns(
                new Dictionary<int, DateTime>
                {
                    { 1245, DateTime.Parse("25/05/2019 09:24:00") },
                    { 1246, DateTime.Parse("25/05/2019 14:26:00") }
                });

            _meterReadingDateHandler = new MeterReadingDateHandler(mockMeterReadings.Object);
        }

        [Test]
        public void MeterReading_NewerReadingExists_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 1245,
                    MeterReadingDateTime = DateTime.Parse("25/05/2019 09:23:59"),
                    MeterReadingValue = "2000"
                }
            };

            _meterReadingDateHandler.Process(meterReadings);

            var error = meterReadings.First().Error;

            Assert.That(error, Is.EqualTo("A newer meter reading exists in the database"));
        }

        [Test]
        public void MeterReading_NoNewerReadingExists_ReturnsNullErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 1245,
                    MeterReadingDateTime = DateTime.Parse("25/05/2019 09:24:01"),
                    MeterReadingValue = "2000"
                }
            };

            _meterReadingDateHandler.Process(meterReadings);

            var error = meterReadings.First().Error;

            Assert.That(error, Is.Null);
        }
    }
}
