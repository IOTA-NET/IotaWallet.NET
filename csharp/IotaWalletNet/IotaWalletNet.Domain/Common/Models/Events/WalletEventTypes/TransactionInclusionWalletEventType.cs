using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes
{
    public class TransactionInclusionWalletEvent
    {
        public TransactionInclusionWalletEvent(string transactionId, string inclusionState)
        {
            TransactionId = transactionId;
            InclusionState = inclusionState;    
        }

        public string TransactionId { get; set; }

        public string InclusionState { get; set; }
    }

    public class TransactionInclusionWalletEventType : IWalletEventType
    {
        public TransactionInclusionWalletEventType(TransactionInclusionWalletEvent transactionInclusion)
        {
            TransactionInclusion = transactionInclusion;    
        }

        public string Type { get; set; } = "TransactionInclusion";

        public TransactionInclusionWalletEvent TransactionInclusion { get; set; }
    }
}
