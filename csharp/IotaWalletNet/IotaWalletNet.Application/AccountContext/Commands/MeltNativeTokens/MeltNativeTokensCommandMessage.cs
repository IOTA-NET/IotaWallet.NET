using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens
{
    public class MeltNativeTokensCommandMessage : AccountMessage<MeltNativeTokensCommandMessageData>
    {
        private const string METHOD_NAME = "decreaseNativeTokenSupply";

        public MeltNativeTokensCommandMessage(string username, MeltNativeTokensCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {
        }
    }
}
