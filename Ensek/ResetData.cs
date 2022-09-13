using DAL;

namespace Ensek
{
    public class ResetData
    {
        private readonly IRepository<AccountDTO> _accounts;

        public ResetData(IRepository<AccountDTO> accounts)
        {
            _accounts = accounts;
        }

        public void CreateAccountData()
        {
            var accounts = new List<AccountDTO>
            {
                new AccountDTO { AccountId = 2344, FirstName = "Tommy", LastName = "Test" },
                new AccountDTO { AccountId = 2233, FirstName = "Barry", LastName = "Test" },
                new AccountDTO { AccountId = 8766, FirstName = "Sally", LastName = "Test" },
                new AccountDTO { AccountId = 2345, FirstName = "Jerry", LastName = "Test" },
                new AccountDTO { AccountId = 2346, FirstName = "Ollie", LastName = "Test" },
                new AccountDTO { AccountId = 2347, FirstName = "Tara", LastName = "Test" },
                new AccountDTO { AccountId = 2348, FirstName = "Tammy", LastName = "Test" },
                new AccountDTO { AccountId = 2349, FirstName = "Simon", LastName = "Test" },
                new AccountDTO { AccountId = 2350, FirstName = "Colin", LastName = "Test" },
                new AccountDTO { AccountId = 2351, FirstName = "Gladys", LastName = "Test" },
                new AccountDTO { AccountId = 2352, FirstName = "Greg", LastName = "Test" },
                new AccountDTO { AccountId = 2353, FirstName = "Tony", LastName = "Test" },
                new AccountDTO { AccountId = 2355, FirstName = "Arthur", LastName = "Test" },
                new AccountDTO { AccountId = 2356, FirstName = "Craig", LastName = "Test" },
                new AccountDTO { AccountId = 6776, FirstName = "Laura", LastName = "Test" },
                new AccountDTO { AccountId = 4534, FirstName = "JOSH", LastName = "TEST" },
                new AccountDTO { AccountId = 1234, FirstName = "Freya", LastName = "Test" },
                new AccountDTO { AccountId = 1239, FirstName = "Noddy", LastName = "Test" },
                new AccountDTO { AccountId = 1240, FirstName = "Archie", LastName = "Test" },
                new AccountDTO { AccountId = 1241, FirstName = "Lara", LastName = "Test" },
                new AccountDTO { AccountId = 1242, FirstName = "Tim", LastName = "Test" },
                new AccountDTO { AccountId = 1243, FirstName = "Graham", LastName = "Test" },
                new AccountDTO { AccountId = 1244, FirstName = "Tony", LastName = "Test" },
                new AccountDTO { AccountId = 1245, FirstName = "Neville", LastName = "Test" },
                new AccountDTO { AccountId = 1246, FirstName = "Jo", LastName = "Test" },
                new AccountDTO { AccountId = 1247, FirstName = "Jim", LastName = "Test" },
                new AccountDTO { AccountId = 1248, FirstName = "Pam", LastName = "Test" },
            };

            _accounts.Create(accounts);
        }
    }
}
