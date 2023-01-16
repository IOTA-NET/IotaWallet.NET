using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommandMessageData
    {
        public SendAmountCommandMessageData(List<AddressWithAmount> addressesWithAmount, TransactionOptions transactionOptions)
        {
            AddressesWithAmount = addressesWithAmount;
            Options = transactionOptions;
        }

        public SendAmountCommandMessageData(TransactionOptions transactionOptions)
            : this(new List<AddressWithAmount>(), transactionOptions)
        {

        }
        public SendAmountCommandMessageData AddAddressAndAmount(string address, string amount)
        {
            AddressWithAmount addressWithAmount = new AddressWithAmount(address, amount);
            AddressesWithAmount.Add(addressWithAmount);

            return this;
        }

        public List<AddressWithAmount> AddressesWithAmount { get; } = new List<AddressWithAmount>();
        public TransactionOptions Options { get; set; } = new TransactionOptions();
    }
}
