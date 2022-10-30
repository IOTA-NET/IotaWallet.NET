using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs
{
    public class ClaimOutputsCommand : IRequest<ClaimOutputsResponse>
    {
        public ClaimOutputsCommand(string username, IAccount account, List<string> outputIds)
        {
            Account = account;
            Username = username;
            OutputIds = outputIds;
        }
        public IAccount Account { get; set; }
        public string Username { get; set; }

        public List<string> OutputIds { get; set; }
    }
}
