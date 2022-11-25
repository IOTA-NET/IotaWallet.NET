using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommand : IRequest<SendAmountResponse>
    {
        public SendAmountCommand(IAccount account, string username, List<AddressWithAmount> addressesWithAmount)
        {
            Account = account;
            Username = username;
            AddressesWithAmount = addressesWithAmount;
        }

        public IAccount Account { get; }
        public string Username { get; }
        public List<AddressWithAmount> AddressesWithAmount { get; set; } = new List<AddressWithAmount>();

    }
}
