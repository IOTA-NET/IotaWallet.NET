using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNft
{
    public class BurnNftCommandMessage : AccountMessage<BurnNftCommandMessageData>
    {
        private const string METHOD_NAME = "burnNft";

        public BurnNftCommandMessage(string username, BurnNftCommandMessageData methodData) 
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
