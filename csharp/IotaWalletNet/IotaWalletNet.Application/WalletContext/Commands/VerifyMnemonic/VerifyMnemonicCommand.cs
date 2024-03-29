﻿using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommand : IRequest<VerifyMnemonicResponse>
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
