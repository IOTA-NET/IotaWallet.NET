using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount
{
    public class SendMicroAmountCommandMessageData
    {
        public SendMicroAmountCommandMessageData(List<AddressWithMicroAmount> addressesWithMicroAmount, TransactionOptions options)
        {
            AddressesWithMicroAmount = addressesWithMicroAmount;
            Options = options;
        }

        public List<AddressWithMicroAmount> AddressesWithMicroAmount { get; set; }

        public TransactionOptions Options { get; set; }
    }
}
