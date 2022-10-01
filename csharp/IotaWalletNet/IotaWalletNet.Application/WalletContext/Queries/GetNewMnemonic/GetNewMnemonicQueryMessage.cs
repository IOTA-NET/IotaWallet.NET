using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic
{
    internal class GetNewMnemonicQueryMessage : Message<string>
    {
        private const string COMMAND = "generateMnemonic";
        public GetNewMnemonicQueryMessage()
        {
            Cmd = COMMAND;
        }
    }
}
