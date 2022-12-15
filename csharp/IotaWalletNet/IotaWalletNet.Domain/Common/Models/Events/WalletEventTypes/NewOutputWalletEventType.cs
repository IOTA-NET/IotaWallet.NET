using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Explorer;
using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;

namespace IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes
{
    public class NewOutputWalletEvent
    {
        public NewOutputWalletEvent(OutputData output, TransactionPayload transaction, OutputResponse transactionInputs)
        {
            Output = output;
            Transaction = transaction;
            TransactionInputs = transactionInputs;
        }

        /// <summary>
        /// The new output.
        /// </summary>
        public OutputData Output { get; set; }

        /// <summary>
        /// The transaction that created the output. Might be pruned and not available.
        /// </summary>
        public TransactionPayload Transaction { get; set; }

        public OutputResponse TransactionInputs { get; set; }
    }

    public class NewOutputWalletEventType : IWalletEventType
    {
        public NewOutputWalletEventType(NewOutputWalletEvent newOutputWalletEvent)
        {
            NewOutput = newOutputWalletEvent;
        }

        public string Type { get; set; } = nameof(NewOutput);

        public NewOutputWalletEvent NewOutput { get; set; }
    }
}
