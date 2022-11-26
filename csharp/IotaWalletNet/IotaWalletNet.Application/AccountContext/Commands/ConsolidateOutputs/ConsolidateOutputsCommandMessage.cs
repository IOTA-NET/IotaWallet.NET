using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs
{
    public class ConsolidateOutputsCommandMessage : AccountMessage<ConsolidateOutputsCommandMessageData>
    {
        private const string METHOD_NAME = "consolidateOutputs";
        public ConsolidateOutputsCommandMessage(string username, ConsolidateOutputsCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
