using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens
{
    public class MeltNativeTokensMessage : AccountMessage<MeltNativeTokensMessageData>
    {
        private const string METHOD_NAME = "decreaseNativeTokenSupply";

        public MeltNativeTokensMessage(string username, MeltNativeTokensMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {
        }
    }
}
