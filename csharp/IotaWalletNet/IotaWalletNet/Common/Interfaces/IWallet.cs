using IotaWalletNet.Domain.Options;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    public interface IWallet
    {
        ClientOptionsBuilder ConfigureClientOptions();
        SecretManagerOptionsBuilder ConfigureSecretManagerOptions();
        WalletOptionsBuilder ConfigureWalletOptions();
        void Connect();
        WalletOptions GetWalletOptions();
        void SendMessage(string message);
    }
}