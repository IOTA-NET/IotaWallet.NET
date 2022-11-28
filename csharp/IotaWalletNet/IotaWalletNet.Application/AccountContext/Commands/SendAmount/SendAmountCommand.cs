using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommand : IRequest<SendAmountResponse>
    {
        public SendAmountCommand(IAccount account, string username, List<AddressWithAmount> addressesWithAmount, TaggedDataPayload? taggedDataPayload)
        {
            Account = account;
            Username = username;
            AddressesWithAmount = addressesWithAmount;
            TaggedDataPayload = taggedDataPayload;

            if (!AddressesWithAmount.Any())
                throw new Exception("You need to specify atleast one address with amount.");
        }

        public IAccount Account { get; }
        public string Username { get; }
        public TaggedDataPayload? TaggedDataPayload { get; set; }
        public List<AddressWithAmount> AddressesWithAmount { get; set; }

    }
}
