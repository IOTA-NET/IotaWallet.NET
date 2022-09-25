using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQuery : IRequest<string>
    {
        public GetBalanceQuery(IWallet wallet, string username)
        {
            Wallet = wallet;
            Username = username;
        }

        public IWallet Wallet { get; }
        public string Username { get; }
    }
}
