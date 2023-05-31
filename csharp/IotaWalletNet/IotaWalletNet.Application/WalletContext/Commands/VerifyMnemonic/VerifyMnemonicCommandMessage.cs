using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    internal class VerifyMnemonicCommandMessage : Message<VerifyMnemonicCommandMessageData>
    {
        private const string COMMAND = "verifyMnemonic";

        public VerifyMnemonicCommandMessage(VerifyMnemonicCommandMessageData verifyMnemonicCommandMessageData)
        {
            Cmd = COMMAND;
            Payload = verifyMnemonicCommandMessageData;
        }
    }
}
