using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommand : IRequest
    {
        public VerifyMnemonicCommand(IWallet wallet, string mnemonic)
        {
            Wallet = wallet;
            Mnemonic = mnemonic;
        }

        public IWallet Wallet { get; }
        public string Mnemonic { get; }
    }
}
