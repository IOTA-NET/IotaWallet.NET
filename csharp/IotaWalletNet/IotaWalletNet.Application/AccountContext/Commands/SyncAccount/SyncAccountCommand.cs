using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public class SyncAccountCommand : IRequest
    {
        public SyncAccountCommand(IWallet wallet, string username)
        {
            Wallet = wallet;
            Username = username;
        }

        public IWallet Wallet { get; }
        public string Username { get; }
    }
}
