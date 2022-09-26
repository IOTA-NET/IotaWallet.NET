using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;

namespace IotaWalletNet.Application.Common.Interfaces
{
    public interface IAccount : IRustBridgeCommunicator
    {
        string Username { get; }

        IWallet Wallet { get; }

        Task<string> SendAmountAsync(AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions);

        Task<string> SyncAccountAsync();

        Task<string> GetBalanceAsync();
        Task RequestFromFaucet(string address, string url);
    }
}
