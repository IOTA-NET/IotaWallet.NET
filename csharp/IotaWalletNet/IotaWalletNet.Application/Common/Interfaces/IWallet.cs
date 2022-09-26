using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Application.Common.Interfaces
{
    public interface IWallet : IRustBridgeCommunicator
    {
        ClientOptionsBuilder ConfigureClientOptions();
        SecretManagerOptionsBuilder ConfigureSecretManagerOptions();
        WalletOptionsBuilder ConfigureWalletOptions();
        IWallet ThenInitialize();
        Task<(string response, IAccount? account)> CreateAccountAsync(string username);
        Task<(string response, IAccount? account)> GetAccountAsync(string username);
        Task<string> GetAccountsAsync();

        Task<GetNewMnemonicQueryResponse> GetNewMnemonicAsync();
        WalletOptions GetWalletOptions();

        Task<string> StoreMnemonicAsync(string mnemonic);
        Task<string> VerifyMnemonicAsync(string mnemonic);
    }
}