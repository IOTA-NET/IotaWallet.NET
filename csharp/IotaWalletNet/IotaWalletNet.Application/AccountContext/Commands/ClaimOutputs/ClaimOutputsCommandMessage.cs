using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs
{
    public class ClaimOutputsCommandMessage : AccountMessage<List<string>>
    {
        private const string METHOD_NAME = "claimOutputs";
        public ClaimOutputsCommandMessage(string username, List<string> outputIds)
            : base(username, METHOD_NAME, outputIds)
        {

        }
    }
}
