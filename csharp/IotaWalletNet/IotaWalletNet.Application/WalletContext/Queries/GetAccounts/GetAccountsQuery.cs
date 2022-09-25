using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsQuery : IRequest<string>
    {
        public GetAccountsQuery(IWallet wallet)
        {
            Wallet = wallet;
        }


        public IWallet Wallet { get; private set; }
    }
}
