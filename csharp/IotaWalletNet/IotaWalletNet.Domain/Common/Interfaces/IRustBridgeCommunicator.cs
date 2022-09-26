namespace IotaWalletNet.Domain.Common.Interfaces
{
    public interface IRustBridgeCommunicator
    {
        Task<string> SendMessageAsync(string message);
        void WalletMessageReceivedCallback(string message, string error, IntPtr context);
    }
}