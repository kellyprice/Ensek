using Ensek.Data;
using Ensek.ViewModels;

namespace Ensek.Library
{
    public class AccountHandler : BaseHandler
    {
        private readonly IAccountRepository _accountRepository;

        public AccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public override void Process(IEnumerable<MeterReadingViewModel> meterReadings)
        {
            var accountIds = meterReadings.Where(x => x.Error == null).Select(x => x.AccountId);
            var invalidAccounts = _accountRepository.InvalidAccounts(accountIds);

            foreach (var meterReading in meterReadings
                .Where(x => invalidAccounts.Contains(x.AccountId) && x.Error == null))
            {
                meterReading.Error = "Invalid account";
            }

            if (_nextHandler != null) _nextHandler.Process(meterReadings);
        }
    }
}
