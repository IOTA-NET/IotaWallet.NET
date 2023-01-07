using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs
{
    public class ClaimOutputsCommandMessage : AccountMessage<ClaimOutputsCommandMessageData>
    {
        private const string METHOD_NAME = "claimOutputs";
        public ClaimOutputsCommandMessage(string username, ClaimOutputsCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
