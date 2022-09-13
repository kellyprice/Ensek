using DAL;

namespace Ensek.Data
{
    public class AccountRepository : IAccountRepository
    {
        public IRepository<AccountDTO> _accounts;

        public AccountRepository(IRepository<AccountDTO> accounts)
        {
            _accounts = accounts;
        }

        public IEnumerable<int> InvalidAccounts(IEnumerable<int> accountIds)
        {
            try
            {
                return accountIds
                    .Except(_accounts
                        .Get(x => accountIds
                            .Contains(x.AccountId))
                        .Select(x => x.AccountId));
            }
            catch
            {
                // log error

                throw;
            }
        }
    }
}
