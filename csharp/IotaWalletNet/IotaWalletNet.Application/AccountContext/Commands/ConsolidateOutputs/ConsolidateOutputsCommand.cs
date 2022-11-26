using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs
{
    public class ConsolidateOutputsCommand : IRequest<ConsolidateOutputsResponse>
    {
        public ConsolidateOutputsCommand(bool force, string username, IAccount account, int? outputConsolidationThreshold=null)
        {
            Force = force;
            OutputConsolidationThreshold = outputConsolidationThreshold;
            Username = username;
            Account = account;
        }

        public bool Force { get; set; }

        public int? OutputConsolidationThreshold { get; set; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
