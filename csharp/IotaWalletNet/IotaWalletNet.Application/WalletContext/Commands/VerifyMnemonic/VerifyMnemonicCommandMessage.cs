using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommandMessage : Message<string>
    {
        private const string COMMAND = "VerifyMnemonic";

        public VerifyMnemonicCommandMessage(string mnemonic)
        {
            Cmd = COMMAND;
            Payload = mnemonic;
        }
    }
}
