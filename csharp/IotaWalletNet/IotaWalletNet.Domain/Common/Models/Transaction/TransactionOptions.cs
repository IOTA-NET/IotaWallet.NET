using IotaWalletNet.Domain.Common.Models.Transaction.Strategy;

namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    public class TransactionOptions
    {
        public RemainderValueStrategy? RemainderValueStrategy { get; set; } = new ReuseAddressStrategy();

        /** Custom inputs that should be used for the transaction */
        public List<string>? CustomInputs { get; set; }

        /** Optional note, that is only stored locally */
        public string? Note { get; set; }
    }
}
