using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBankingAPI.Contracts.CustomerBankAccount;
using TestBankingAPI.Contracts.DBModel;
using TestBankingAPI.Contracts.DTO;
using TestBankingAPI.Model;

namespace TestBankingAPI.Services.CustomerBankAccount
{
    public class CustomerBankAccountService : ICustomerBankAccountService
    {
        private readonly AppDBContext appDBContext;
        public CustomerBankAccountService(AppDBContext _appDBContext)
        {
            this.appDBContext = _appDBContext;
        }
        public async Task<CustomerBankAccount_Details> CustomerAccountDetailsByID(int id)
        {
            var result = await appDBContext.Customers
                         .Include(a=>a.Accounts)
                         .FirstOrDefaultAsync(c => c.CustomerID == id);
            
            ICollection<AccountDTO> account_Lst = new List<AccountDTO>();
            foreach(var account in result.Accounts) 
            {
                AccountDTO ac = new AccountDTO();
                ac.CustomerID = account.CustomerID;
                ac.AccountID = account.AccountID;
                ac.Balance = account.Balance;
                account_Lst.Add(ac);
            }
            CustomerBankAccount_Details cust = new CustomerBankAccount_Details(result.CustomerID,result.Name, account_Lst);
            return cust;
        }

        public async Task CreateCustomerAndBankAccount(CustomerBankAccount_Details customer)
        {
            Customer cust = new Customer();
            ICollection<Account> account_Lst = new List<Account>();
            cust.CustomerID = customer.CustomerID;
            cust.Name = customer.CustomerName;
            foreach (var account in customer.Accounts)
            {
                Account ac = new Account();
                ac.CustomerID = account.CustomerID;
                ac.AccountID = account.AccountID;
                ac.Balance = account.Balance;
                account_Lst.Add(ac);
                cust.Accounts = account_Lst;
            }


            var ExistingCustomer = await appDBContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == customer.CustomerID);
            if (ExistingCustomer != null) // update existing customer.
            {
                ExistingCustomer.Name = cust.Name;
                ExistingCustomer.Accounts = cust.Accounts;
                await appDBContext.SaveChangesAsync();
            }
            else //Add New Customer.
            {
                var result = await appDBContext.Customers.AddAsync(cust);
                await appDBContext.SaveChangesAsync();
            }
        }
    }
}
