using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNativeTokens
{
    public class BurnNativeTokensCommandMessage : AccountMessage<BurnNativeTokensCommandMessageData>
    {
        private const string METHOD_NAME = "burnNativeToken";

        public BurnNativeTokensCommandMessage(string username, BurnNativeTokensCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {
        }
    }
}
