using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    public class StoreMnemonicCommand : IRequest<StoreMnemonicResponse>
    {
        public StoreMnemonicCommand(IWallet wallet, string mnemonic)
        {
            Wallet = wallet;
            Mnemonic = mnemonic;
        }

        public string Mnemonic { get; private set; }

        public IWallet Wallet { get; private set; }
    }
}
