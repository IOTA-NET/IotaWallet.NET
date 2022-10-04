using IotaWalletNet.Application.Common.Interfaces;
using MediatR;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    public class GetAccountQuery : IRequest<GetAccountResponse>
    {
        public GetAccountQuery(IWallet wallet, string username)
        {
            Wallet = wallet;
            Username = username;
        }

        public IWallet Wallet { get; }
        public string Username { get; }
    }
}
