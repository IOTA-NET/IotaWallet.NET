using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount
{
    public class SendMicroAmountCommand : IRequest<SendMicroAmountResponse>
    {
        public SendMicroAmountCommand(List<AddressWithMicroAmount> addressesWithMicroAmount, string username, IAccount account)
        {
            AddressesWithMicroAmount = addressesWithMicroAmount;
            Username = username;
            Account = account;
        }

        public List<AddressWithMicroAmount> AddressesWithMicroAmount { get; set; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
