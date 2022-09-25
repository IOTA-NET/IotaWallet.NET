using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommand : IRequest<string>
    {
        public SendAmountCommand(IWallet wallet, string username, AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions)
        {
            Wallet = wallet;
            Username = username;
            AddressesWithAmountAndTransactionOptions = addressesWithAmountAndTransactionOptions;
        }

        public IWallet Wallet { get; }
        public string Username { get; }
        public AddressesWithAmountAndTransactionOptions AddressesWithAmountAndTransactionOptions { get; }
    }
}
