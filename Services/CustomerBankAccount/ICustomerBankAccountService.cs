using TestBankingAPI.Contracts.CustomerBankAccount;
using TestBankingAPI.Contracts.DBModel;
using TestBankingAPI.Model;

namespace TestBankingAPI.Services.CustomerBankAccount
{
    public interface ICustomerBankAccountService
    {
        Task<CustomerBankAccount_Details> CustomerAccountDetailsByID(int id);
        Task CreateCustomerAndBankAccount(CustomerBankAccount_Details request);
    }
}
