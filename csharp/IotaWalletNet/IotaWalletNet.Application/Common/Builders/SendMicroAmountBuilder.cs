using IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using MediatR;

namespace IotaWalletNet.Application.Common.Builders
{
    public class SendMicroAmountBuilder
    {
        private readonly List<AddressWithMicroAmount> _addressWithMicroAmounts;
        private readonly IMediator _mediator;
        private readonly IAccount _account;
        private readonly string _username;
        private TaggedDataPayload? _taggedDataPayload;

        public SendMicroAmountBuilder(IMediator mediator, IAccount account, string username)
        {
            _addressWithMicroAmounts = new List<AddressWithMicroAmount>();
            _mediator = mediator;
            _account = account;
            _username = username;
        }

        public SendMicroAmountBuilder AddAddressAndAmount(string receiverAddress, ulong amountInGlow)
        {
            AddressWithMicroAmount addressWithMicroAmount = new AddressWithMicroAmount(receiverAddress, amountInGlow.ToString(), expiration:0);
            _addressWithMicroAmounts.Add(addressWithMicroAmount);

            return this;
        }

        public SendMicroAmountBuilder SetTaggedDataPayload(string tag, string data)
        {
            _taggedDataPayload = new TaggedDataPayload(tag, data);
            return this;
        }
        public async Task<SendMicroAmountResponse> SendMicroAmountAsync()
        {
            return await _mediator.Send(new SendMicroAmountCommand(_addressWithMicroAmounts, _taggedDataPayload, _username, _account));
        }
    }
}
