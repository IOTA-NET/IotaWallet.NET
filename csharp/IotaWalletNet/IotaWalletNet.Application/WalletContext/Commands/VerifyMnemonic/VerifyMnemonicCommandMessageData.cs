namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    internal class VerifyMnemonicCommandMessageData
    {
        public VerifyMnemonicCommandMessageData(string mnemonic)
        {
            Mnemonic = mnemonic;
        }

        public string Mnemonic { get; set; }
    }
}
