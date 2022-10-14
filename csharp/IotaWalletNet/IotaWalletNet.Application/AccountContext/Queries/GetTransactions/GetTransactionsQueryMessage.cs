using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransactions
{
    public class GetTransactionsQueryMessage : AccountMessage
    {
        private const string METHOD_NAME = "transactions";
        public GetTransactionsQueryMessage(string username)
            : base(username, METHOD_NAME)
        {

        }
    }
}
