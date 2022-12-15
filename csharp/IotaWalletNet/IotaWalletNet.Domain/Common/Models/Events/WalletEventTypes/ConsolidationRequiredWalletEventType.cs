using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes
{

    public class ConsolidationRequiredWalletEvent
    {

    }

    public class ConsolidationRequiredWalletEventType : IWalletEventType
    {
        public ConsolidationRequiredWalletEventType(ConsolidationRequiredWalletEvent consolidationRequired)
        {
            ConsolidationRequired = consolidationRequired;
        }

        public string Type { get; set; } = "ConsolidationRequired";

        public ConsolidationRequiredWalletEvent ConsolidationRequired { get; set; }
    }
}
