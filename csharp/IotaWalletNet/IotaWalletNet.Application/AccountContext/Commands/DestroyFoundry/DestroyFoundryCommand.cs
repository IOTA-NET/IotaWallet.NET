using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.DestroyFoundry
{
    public class DestroyFoundryCommand : IRequest<DestroyFoundryResponse>
    {
        public DestroyFoundryCommand(string foundryId, string username, IAccount account)
        {
            FoundryId = foundryId;
            Username = username;
            Account = account;
        }

        public string FoundryId { get; set; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
