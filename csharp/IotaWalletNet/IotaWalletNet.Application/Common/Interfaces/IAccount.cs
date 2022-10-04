using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Network;
using static IotaWalletNet.Application.AccountContext.Commands.SyncAccount.SyncAccountCommandHandler;

namespace IotaWalletNet.Application.Common.Interfaces
{
    public interface IAccount : IRustBridgeCommunicator
    {
        string Username { get; }

        IWallet Wallet { get; }

        Task<string> SendAmountAsync(AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions);

        Task<SyncAccountResponse> SyncAccountAsync();

        Task<string> GetBalanceAsync();
        Task RequestFromFaucetAsync(string address, string url);
        Task<GenerateAddressesResponse> GenerateAddressesAsync(uint numberOfAddresses = 1, NetworkType networkType = default);
    }
}
