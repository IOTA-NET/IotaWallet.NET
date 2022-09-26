using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public class SyncAccountCommand : IRequest<string>
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
