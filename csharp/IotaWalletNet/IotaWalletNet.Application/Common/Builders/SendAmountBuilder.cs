using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using MediatR;

namespace IotaWalletNet.Application.Common.Builders
{
    public class SendAmountBuilder
    {
        private readonly List<AddressWithAmount> _addressWithAmounts;
        private readonly IMediator _mediator;
        private readonly IAccount _account;
        private readonly string _username;
        private TaggedDataPayload? _taggedDataPayload;

        public SendAmountBuilder(IMediator mediator, IAccount account, string username)
        {
            _addressWithAmounts= new List<AddressWithAmount>();
            _mediator = mediator;
            _account = account;
            _username = username;
        }

        public SendAmountBuilder AddAddressAndAmount(string receiverAddress, ulong amountInGlow)
        {
            AddressWithAmount addressWithAmount = new AddressWithAmount(receiverAddress, amountInGlow.ToString());
            _addressWithAmounts.Add(addressWithAmount);

            return this;
        }

        public SendAmountBuilder SetTaggedDataPayload(string tag, string data)
        {
            _taggedDataPayload = new TaggedDataPayload(tag, data);
            return this;
        }
        public async Task<SendAmountResponse> SendAmountAsync()
        {
            return await _mediator.Send(new SendAmountCommand(_account, _username, _addressWithAmounts, _taggedDataPayload));
        }
    }
}