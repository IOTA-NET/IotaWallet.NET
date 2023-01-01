using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount
{
    public class SendMicroAmountCommand : IRequest<SendMicroAmountResponse>
    {
        public SendMicroAmountCommand(List<AddressWithMicroAmount> addressesWithMicroAmount, TaggedDataPayload? taggedDataPayload, string username, IAccount account)
        {
            AddressesWithMicroAmount = addressesWithMicroAmount;
            Username = username;
            Account = account;
            TaggedDataPayload= taggedDataPayload;
        }

        public List<AddressWithMicroAmount> AddressesWithMicroAmount { get; set; }

        public TaggedDataPayload? TaggedDataPayload { get; set; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
