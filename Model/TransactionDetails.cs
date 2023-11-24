namespace TestBankingAPI.Model
{
    public class TransactionDetails
    {
        public int TransactionID { get; }
        public int SenderAccountID { get; }
        public int RecieverAccountID { get; }
        public decimal StartBalance { get; }
        public decimal TransactionAmount { get; }
        public decimal EndBalance { get; }
        public DateTime TransactionTime { get; }
        public TransactionDetails(int transactionID, int senderAccountID,int recieverAccountID, decimal startBalance, decimal transactionAmount, decimal endBalance, DateTime transactionTime)
        {
            TransactionID = transactionID;
            SenderAccountID = senderAccountID;
            RecieverAccountID= recieverAccountID;
            StartBalance = startBalance;
            TransactionAmount = transactionAmount;
            EndBalance = endBalance;
            TransactionTime = transactionTime;
        }
    }
}
