namespace IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens
{
    public class MintNAtiveTokensCommandMessage
    {
        private const string METHOD_NAME = "mintNativeToken";

        public MintNAtiveTokensCommandMessage(MintNativeTokensCommandMessageData data)
        {
            Data = data;
        }

        public MintNativeTokensCommandMessageData Data { get; set; }
    }

}
