using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens
{
    public class SendNativeTokensCommandMessage : AccountMessage<SendNativeTokensCommandMessageData>
    {
        private const string METHOD_NAME = "sendNativeTokens";
        public SendNativeTokensCommandMessage(string username, SendNativeTokensCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
