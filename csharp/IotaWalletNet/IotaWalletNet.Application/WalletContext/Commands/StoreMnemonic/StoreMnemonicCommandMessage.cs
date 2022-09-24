using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    internal class StoreMnemonicCommandMessage : Message<string>
    {
        private const string COMMAND = "StoreMnemonic";

        public StoreMnemonicCommandMessage(string mnemonic)
        {
            Cmd = COMMAND;
            Payload = mnemonic;
        }
    }
}
