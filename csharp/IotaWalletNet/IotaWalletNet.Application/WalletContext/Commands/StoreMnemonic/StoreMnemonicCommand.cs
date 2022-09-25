using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    public class StoreMnemonicCommand : IRequest<string>
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
