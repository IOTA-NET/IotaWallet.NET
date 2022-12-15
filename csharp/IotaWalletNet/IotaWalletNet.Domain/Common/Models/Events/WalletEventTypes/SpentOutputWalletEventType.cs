using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes
{
    public class SpentOutputWalletEvent
    {
        public SpentOutputWalletEvent(OutputData output)
        {
            Output = output;
        }

        public OutputData Output { get; set; }
    }
    public class SpentOutputWalletEventType : IWalletEventType
    {
        public SpentOutputWalletEventType(SpentOutputWalletEvent spentOutput)
        {
            SpentOutput = spentOutput;
        }

        public string Type { get; set; } = nameof(SpentOutput);
        public SpentOutputWalletEvent SpentOutput { get; set; }

    }
}
