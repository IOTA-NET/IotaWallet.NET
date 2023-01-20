using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransaction
{
    public class GetTransactionQueryMessage : AccountMessage<GetTransactionQueryMessageData>
    {
        private const string METHOD_NAME = "getTransaction";
        public GetTransactionQueryMessage(string username, GetTransactionQueryMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
