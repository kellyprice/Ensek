using Ensek.ViewModels;

namespace Ensek.Library
{
    public class Validate
    {
        private readonly AccountHandler _accountHandler;
        private readonly DuplicateHandler _duplicateHandler;
        private readonly FileDuplicateHandler _fileDuplicateHandler;
        private readonly MeterReadingDateHandler _meterReadingDateHandler;
        private readonly MeterReadingHandler _meterReadingHandler;

        public Validate(AccountHandler accountHandler, DuplicateHandler duplicateHandler,
            FileDuplicateHandler fileDuplicateHandler, MeterReadingDateHandler meterReadingDateHandler,
            MeterReadingHandler meterReadingHandler)
        {
            _accountHandler = accountHandler;
            _duplicateHandler = duplicateHandler;
            _fileDuplicateHandler = fileDuplicateHandler;
            _meterReadingDateHandler = meterReadingDateHandler;
            _meterReadingHandler = meterReadingHandler;
        }

        public void Process(IEnumerable<MeterReadingViewModel> meterReading)
        {
            _meterReadingHandler.SetNextHandler(_accountHandler);
            _accountHandler.SetNextHandler(_fileDuplicateHandler);
            _fileDuplicateHandler.SetNextHandler(_duplicateHandler);
            _duplicateHandler.SetNextHandler(_meterReadingDateHandler);

            _meterReadingHandler.Process(meterReading);
        }
    }
}
