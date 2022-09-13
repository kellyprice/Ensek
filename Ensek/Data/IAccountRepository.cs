namespace Ensek.Data
{
    public interface IAccountRepository
    {
        IEnumerable<int> InvalidAccounts(IEnumerable<int> accountIds);
    }
}
