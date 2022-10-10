using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNfts
{
    public class SendNftsCommandMessage : AccountMessage<SendNftsCommandMessageData>
    {
        private const string METHOD_NAME = "sendNft";
        public SendNftsCommandMessage(string username, SendNftsCommandMessageData sendNftsCommandMessageData)
            : base(username, METHOD_NAME, sendNftsCommandMessageData)
        {

        }
    }
}
