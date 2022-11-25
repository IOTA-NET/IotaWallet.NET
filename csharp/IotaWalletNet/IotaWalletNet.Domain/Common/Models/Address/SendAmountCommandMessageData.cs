using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class SendAmountCommandMessageData
    {
        public SendAmountCommandMessageData(List<AddressWithAmount> addressesWithAmount, TransactionOptions transactionOptions)
        {
            AddressesWithAmount = addressesWithAmount;
            TransactionOptions = transactionOptions;
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
        public TransactionOptions TransactionOptions { get; set; } = new TransactionOptions();
    }
}
