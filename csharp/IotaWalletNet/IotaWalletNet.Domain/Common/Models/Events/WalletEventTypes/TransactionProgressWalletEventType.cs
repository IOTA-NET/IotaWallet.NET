using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes
{

    public class TransactionProgressWalletEventType : IWalletEventType
    {
        public TransactionProgressWalletEventType(string transactionProgress)
        {
            TransactionProgress = transactionProgress;
        }

        public string Type { get; set; } = "TransactionProgress";

        public string TransactionProgress { get; set; }
    }
}
