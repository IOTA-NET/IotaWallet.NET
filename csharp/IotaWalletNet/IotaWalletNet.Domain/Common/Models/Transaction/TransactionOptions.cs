using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
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

        public TaggedDataPayload? TaggedDataPayload { get; set; }

        
        public TransactionOptions AddTaggedDataPayload(string tag, string payload)
        {
            TaggedDataPayload = new TaggedDataPayload(tag, payload);
            return this;
        }

        public TransactionOptions SetNote(string note)
        {
            Note = note;
            return this;
        }

        public TransactionOptions AddCustomInputs(string input)
        {
            if (CustomInputs == null)
                CustomInputs = new List<string>();
            CustomInputs.Add(input);

            return this;
        }
    }
}
