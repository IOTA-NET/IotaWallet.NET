namespace IotaWalletNet.Application.AccountContext.Queries.GetTransaction
{
    public class GetTransactionQueryMessageData
    {
        public GetTransactionQueryMessageData(string transactionId)
        {
            TransactionId = transactionId;
        }

        public string TransactionId { get; set; }
    }
}
