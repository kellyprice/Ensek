using DAL;
using Ensek.Data;
using Ensek.Library;
using Ensek.ViewModels;
using Moq;

namespace Ensek.Tests
{
    public class DuplicateHandlerTests
    {
        private readonly DuplicateHandler _duplicateHandler;

        public DuplicateHandlerTests()
        {
            var mockMeterReadings = new Mock<IMeterReadingRepository>();

            mockMeterReadings.Setup(m => m.PossibleDuplicates(It.IsAny<IEnumerable<MeterReadingViewModel>>())).Returns(
                new List<MeterReadingDTO>
                {
                    new MeterReadingDTO { AccountId = 123, MeterReadingDateTime = DateTime.Parse("25/05/2019 09:23:59"), MeterReadingValue = "01234" },
                    new MeterReadingDTO { AccountId = 456, MeterReadingDateTime = DateTime.Parse("25/05/2019 09:23:59"), MeterReadingValue = "04321" },
                });

            _duplicateHandler = new DuplicateHandler(mockMeterReadings.Object);
        }

        [Test]
        public void MeterReading_DuplicateExists_ReturnsErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 456,
                    MeterReadingDateTime = DateTime.Parse("25/05/2019 09:23:59"),
                    MeterReadingValue = "04321"
                }
            };

            _duplicateHandler.Process(meterReadings);

            var error = meterReadings.First().Error;

            Assert.That(error, Is.EqualTo("This reading is a duplicate of another reading in the database"));
        }

        [Test]
        public void MeterReading_NoDuplicateExists_ReturnsNullErrorMessage()
        {
            IEnumerable<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>
            {
                new MeterReadingViewModel {
                    AccountId = 456,
                    MeterReadingDateTime = DateTime.Parse("25/05/2019 09:23:59"),
                    MeterReadingValue = "04320"
                }
            };

            _duplicateHandler.Process(meterReadings);

            var error = meterReadings.First().Error;

            Assert.That(error, Is.Null);
        }
    }
}
