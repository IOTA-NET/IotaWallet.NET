using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    public interface IRustBridgeCommunicator
    {
        Task<RustBridgeGenericResponse> SendMessageAsync(string message);
        void WalletMessageReceivedCallback(string message, string error, IntPtr context);
    }
}