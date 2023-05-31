namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    internal class GetAccountQueryMessageData
    {
        public GetAccountQueryMessageData(string username)
        {
            AccountId= username;
        }

        public string AccountId { get; private set; }
    }
}
