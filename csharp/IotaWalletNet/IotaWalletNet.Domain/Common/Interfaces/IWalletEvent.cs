namespace IotaWalletNet.Domain.Common.Interfaces
{
    public interface IWalletEvent
    {
        public int AccountIndex { get; set; }

        public IWalletEventType Event { get; set; }
    }
}