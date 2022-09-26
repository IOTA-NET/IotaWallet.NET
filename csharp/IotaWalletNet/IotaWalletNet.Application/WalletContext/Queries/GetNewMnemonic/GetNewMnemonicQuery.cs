using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic
{
    public class GetNewMnemonicQuery : IRequest<GetNewMnemonicQueryResponse>
    {
        public GetNewMnemonicQuery(IWallet wallet)
        {
            Wallet = wallet;
        }

        public IWallet Wallet { get; }
    }
}
