using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using TestBankingAPI.Contracts.DBModel;
using TestBankingAPI.Model;

namespace TestBankingAPI.Services.BankTransactions
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDBContext appDBContext;
        public TransactionService(AppDBContext _appDBContext)
        {
            this.appDBContext = _appDBContext;
        }
        public async Task CreateTransaction(TransactionDetails transactionDetails)
        {
            //Sender Process
            //isBalanceAvailable
            //Update Start Balance(AccountID,Transaction Amount)
            //Update EndBalance(AccountID,Transaction Amount)            

            if (transactionDetails.SenderAccountID != null && transactionDetails.RecieverAccountID != null) 
            {
                bool isBalance = isBalanceAvailable(transactionDetails.SenderAccountID, transactionDetails.TransactionAmount);
                if(isBalance) 
                {
                    #region Sender Transaction
                    Transaction senderTransaction = new Transaction();
                    senderTransaction.TransactionID = transactionDetails.TransactionID;
                    senderTransaction.AccountID = transactionDetails.SenderAccountID;
                    decimal senderCurrentBalance = GetCurrentBalance(transactionDetails.SenderAccountID);
                    senderTransaction.StartBalance = senderCurrentBalance;
                    senderTransaction.TransactionAmount = - transactionDetails.TransactionAmount; //negative Amount, Amount will be deducted from Sender Account.
                    senderTransaction.EndBalance = UpdateEndBalance(senderCurrentBalance, transactionDetails.TransactionAmount);
                    senderTransaction.TransactionTime = transactionDetails.TransactionTime;
                    await appDBContext.Transactions.AddAsync(senderTransaction);
                    await appDBContext.SaveChangesAsync();
                    #endregion

                    #region Reciever Transaction
                    Transaction recieverTransaction = new Transaction();
                    recieverTransaction.TransactionID = transactionDetails.TransactionID;
                    recieverTransaction.AccountID = transactionDetails.RecieverAccountID;
                    decimal recieverCurrentBalance = GetCurrentBalance(transactionDetails.RecieverAccountID);
                    recieverTransaction.StartBalance = recieverCurrentBalance;
                    recieverTransaction.TransactionAmount = transactionDetails.TransactionAmount; //positive Amount, Amount will be added to Reciever Account.
                    recieverTransaction.EndBalance = UpdateEndBalance(recieverCurrentBalance, transactionDetails.TransactionAmount);
                    recieverTransaction.TransactionTime = transactionDetails.TransactionTime;
                    await appDBContext.Transactions.AddAsync(recieverTransaction);
                    await appDBContext.SaveChangesAsync();
                    #endregion
                }
            }
        }
        private bool isBalanceAvailable(int AccountID, decimal transactionAmount) 
        {
            var balance = from t in appDBContext.Transactions
                          where t.AccountID == AccountID
                          orderby t.TransactionID descending
                          select t.EndBalance;
            
            if(Convert.ToDecimal(balance) > transactionAmount)
                return true;

            return false;
        }
        private decimal GetCurrentBalance(int AccountID)
        {
            var balance = from t in appDBContext.Transactions
                          where t.AccountID == AccountID
                          orderby t.TransactionID descending
                          select t.EndBalance;

            
            //if this is the first transaction, our transaction table is empty, get balance from account
            if (balance == null) 
            {
                var AccountBalance =  from acc in appDBContext.Accounts
                                      where acc.AccountID == AccountID
                                      select acc.Balance;
                return Convert.ToDecimal(AccountBalance);
            }
            return Convert.ToDecimal(balance);
        }
        private decimal UpdateEndBalance(decimal balance, decimal transactionAmount)
        {
            return balance+transactionAmount;
        }
        public async Task<IEnumerable<Transaction>> GetTransferHistory(int AccountID)
        {           
            if (AccountID != null) 
            {
             return  await appDBContext.Transactions.Where(t => t.AccountID == AccountID).ToListAsync(); 
            }
            return null;
        }
    }
}
