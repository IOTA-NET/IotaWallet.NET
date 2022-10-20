using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransaction
{
    public class GetTransactionQuery : IRequest<GetTransactionResponse>
    {
        public GetTransactionQuery(string username, IAccount account, string transactionId)
        {
            Username = username;
            Account = account;
            TransactionId = transactionId;
        }

        public string Username { get; set; }

        public IAccount Account { get; set; }

        public string TransactionId { get; set; }
    }
}
