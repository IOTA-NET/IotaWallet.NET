using IotaWalletNet.Application.Common.Interfaces;
using MediatR;
using static IotaWalletNet.Application.AccountContext.Commands.SyncAccount.SyncAccountCommandHandler;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public class SyncAccountCommand : IRequest<SyncAccountResponse>
    {
        public SyncAccountCommand(IAccount account, string username)
        {
            Account = account;
            Username = username;
        }

        public IAccount Account { get; }
        public string Username { get; }
    }
}
