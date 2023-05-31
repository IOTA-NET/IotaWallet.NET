using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    internal class StoreMnemonicCommandMessage : Message<StoreMnemonicCommandMessageData>
    {
        private const string COMMAND = "storeMnemonic";

        public StoreMnemonicCommandMessage(StoreMnemonicCommandMessageData storeMnemonicCommandMessageData)
        {
            Cmd = COMMAND;
            Payload = storeMnemonicCommandMessageData;
        }
    }
}
