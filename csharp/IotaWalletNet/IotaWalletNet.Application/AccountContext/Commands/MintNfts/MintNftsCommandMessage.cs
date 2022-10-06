using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNfts
{
    public class MintNftsCommandMessage : AccountMessage<MintNftsCommandMessageData>
    {
        private const string METHOD_NAME = "mintNfts";

        public MintNftsCommandMessage(string username, MintNftsCommandMessageData mintNftsCommandMessageData)
            : base(username, METHOD_NAME, mintNftsCommandMessageData)
        {

        }
    }
}
