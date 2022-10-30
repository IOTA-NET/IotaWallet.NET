using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetPendingTransactions
{
    public class GetPendingTransactionsQueryMessage : AccountMessage
    {
        private const string METHOD_NAME = "pendingTransactions";

        public GetPendingTransactionsQueryMessage(string username)
            : base(username, METHOD_NAME)
        {

        }
    }
}
