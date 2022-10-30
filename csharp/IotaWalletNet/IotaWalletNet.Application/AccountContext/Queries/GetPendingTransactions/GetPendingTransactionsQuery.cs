using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetPendingTransactions
{
    public class GetPendingTransactionsQuery : IRequest<GetPendingTransactionsResponse>
    {
        public GetPendingTransactionsQuery(string username, IAccount account)
        {
            Username = username;
            Account = account;
        }

        public string Username { get; set; }
        public IAccount Account { get; set; }
    }
}
