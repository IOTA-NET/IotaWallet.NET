using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<GetTransactionsResponse>
    {
        public string Username { get; set; }

        public IAccount Account { get; set; }

        public GetTransactionsQuery(string username, IAccount account)
        {
            Username = username;
            Account = account;
        }
    }
}
