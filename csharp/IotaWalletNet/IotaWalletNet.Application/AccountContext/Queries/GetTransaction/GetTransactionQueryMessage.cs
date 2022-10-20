using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransaction
{
    public class GetTransactionQueryMessage : AccountMessage<string>
    {
        private const string METHOD_NAME = "getTransaction";
        public GetTransactionQueryMessage(string username, string transactionId)
            : base(username, METHOD_NAME, transactionId)
        {

        }
    }
}
