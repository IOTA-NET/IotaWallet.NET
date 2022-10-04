using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Interfaces;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Application.Common.Interfaces
{
    public interface IWallet : IRustBridgeCommunicator, IDisposable
    {
        ClientOptionsBuilder ConfigureClientOptions();
        SecretManagerOptionsBuilder ConfigureSecretManagerOptions();
        WalletOptionsBuilder ConfigureWalletOptions();
        IWallet ThenInitialize();
        Task<(CreateAccountResponse response, IAccount? account)> CreateAccountAsync(string username);
        Task<(GetAccountResponse response, IAccount? account)> GetAccountAsync(string username);
        Task<string> GetAccountsAsync();

        Task<GetNewMnemonicResponse> GetNewMnemonicAsync();
        WalletOptions GetWalletOptions();

        IntPtr GetWalletHandle();

        Task<StoreMnemonicResponse> StoreMnemonicAsync(string mnemonic);
        Task<VerifyMnemonicResponse> VerifyMnemonicAsync(string mnemonic);
    }
}