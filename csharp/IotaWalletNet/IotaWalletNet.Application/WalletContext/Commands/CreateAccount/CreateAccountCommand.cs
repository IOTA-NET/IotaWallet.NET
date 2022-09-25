using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.WalletContext.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<string>
    {
        public CreateAccountCommand(IWallet wallet, string username)
        {
            Wallet = wallet;
            Username = username;
        }

        public IWallet Wallet { get; }
        public string Username { get; }
    }
}
