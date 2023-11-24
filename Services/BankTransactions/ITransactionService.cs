using TestBankingAPI.Contracts.DBModel;
using TestBankingAPI.Model;

namespace TestBankingAPI.Services.BankTransactions
{
    public interface ITransactionService
    {
        Task CreateTransaction(TransactionDetails transactionDetails);
        Task<IEnumerable<Transaction>> GetTransferHistory(int AccountID);

        // Get Current Balance
    }
}
