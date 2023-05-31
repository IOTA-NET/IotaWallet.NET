namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    internal class StoreMnemonicCommandMessageData
    {
        public StoreMnemonicCommandMessageData(string mnemonic)
        {
            Mnemonic= mnemonic;
        }

        public string Mnemonic { get; private set; }
    }
}
