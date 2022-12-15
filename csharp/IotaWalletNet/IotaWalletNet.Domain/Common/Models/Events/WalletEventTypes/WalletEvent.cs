using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes
{
    public class WalletEvent : IWalletEvent
    {
        public WalletEvent(IWalletEventType @event)
        {
            Event = @event;
        }

        public int AccountIndex { get; set; } = 0;

        public IWalletEventType Event { get; set; }
    }
}
