using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQuery : IRequest<string>
    {
        public GetBalanceQuery(IAccount account, string username)
        {
            Account = account;
            Username = username;
        }

        public IAccount Account { get; }
        public string Username { get; }
    }
}
