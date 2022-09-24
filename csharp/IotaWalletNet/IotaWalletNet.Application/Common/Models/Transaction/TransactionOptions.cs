using IotaWalletNet.Application.Common.Models.Transaction.Strategy;

namespace IotaWalletNet.Application.Common.Models.Transaction
{
    public class TransactionOptions
    {
        public RemainderValueStrategy? RemainderValueStrategy { get; set; } = new ReuseAddressStrategy();

        /** Custom inputs that should be used for the transaction */
        public List<string> CustomInputs { get; set; } = new List<string>();

        /** Optional note, that is only stored locally */
        public string? Note { get; set; }
    }
}
