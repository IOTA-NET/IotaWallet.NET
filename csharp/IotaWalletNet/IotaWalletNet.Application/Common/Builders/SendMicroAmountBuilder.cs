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
        private readonly IAccount _account;
        private TaggedDataPayload? _taggedDataPayload;

        public SendMicroAmountBuilder(IAccount account)
        {
            _addressWithMicroAmounts = new List<AddressWithMicroAmount>();
            _account = account;
        }

        public SendMicroAmountBuilder AddAddressAndAmount(string receiverAddress, ulong amountInGlow, ulong expirationInSeconds)
        {
            AddressWithMicroAmount addressWithMicroAmount = new AddressWithMicroAmount(receiverAddress, amountInGlow.ToString(), expirationInSeconds);
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
            return await _account.SendMicroAmountAsync(_addressWithMicroAmounts, _taggedDataPayload);
        }
    }
}
