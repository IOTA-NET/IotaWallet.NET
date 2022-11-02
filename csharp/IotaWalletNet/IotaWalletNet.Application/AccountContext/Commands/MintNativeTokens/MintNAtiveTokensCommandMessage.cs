using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens
{
    public class MintNativeTokensCommandMessage : AccountMessage<MintNativeTokensCommandMessageData>
    {
        private const string METHOD_NAME = "mintNativeToken";

        public MintNativeTokensCommandMessage(string username, MintNativeTokensCommandMessageData data)
            : base(username, METHOD_NAME, data)
        {

        }
    }
}
