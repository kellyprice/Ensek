using Ensek.ViewModels;
using System.Reflection.Metadata;

namespace Ensek.Library
{
    public abstract class BaseHandler : IHandler
    {
        protected IHandler? _nextHandler;

        public BaseHandler()
        {
            _nextHandler = null;
        }

        public void SetNextHandler(IHandler handler)
        {
            _nextHandler = handler;
        }

        public abstract void Process(IEnumerable<MeterReadingViewModel> meterReadings);
    }
}
