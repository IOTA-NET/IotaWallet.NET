using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressesWithAmountAndTransactionOptions
    {
        public AddressesWithAmountAndTransactionOptions(List<AddressWithAmount> addressWithAmounts)
        {
            AddressesWithAmount = addressWithAmounts;
        }

        public List<AddressWithAmount> AddressesWithAmount { get; }
        public TransactionOptions TransactionOptions { get; set; } = new TransactionOptions();
    }
}
