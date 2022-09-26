using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommand : IRequest<string>
    {
        public SendAmountCommand(IAccount account, string username, AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions)
        {
            Account = account;
            Username = username;
            AddressesWithAmountAndTransactionOptions = addressesWithAmountAndTransactionOptions;
        }

        public IAccount Account { get; }
        public string Username { get; }
        public AddressesWithAmountAndTransactionOptions AddressesWithAmountAndTransactionOptions { get; }
    }
}
