using TestBankingAPI.Contracts.DBModel;
using TestBankingAPI.Contracts.DTO;

namespace TestBankingAPI.Model
{
    public class CustomerBankAccount_Details
    {
        public int CustomerID { get; }
        public string CustomerName { get; }
        public ICollection<AccountDTO> Accounts { get; }
        public CustomerBankAccount_Details(
            int customerId,
            string customerName,
            ICollection<AccountDTO> accounts)
        {
            CustomerID = customerId;
            CustomerName = customerName;
            Accounts = accounts;
        }
    }
}
