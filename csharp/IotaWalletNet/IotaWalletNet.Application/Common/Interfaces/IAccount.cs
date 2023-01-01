using IotaWalletNet.Application.AccountContext.Commands.BurnNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.BurnNft;
using IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs;
using IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs;
using IotaWalletNet.Application.AccountContext.Commands.CreateAliasOutput;
using IotaWalletNet.Application.AccountContext.Commands.DestroyFoundry;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.MintNfts;
using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount;
using IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.SendNfts;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetAddresses;
using IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.AccountContext.Queries.GetFoundryOutput;
using IotaWalletNet.Application.AccountContext.Queries.GetMinimumStorageDepositRequired;
using IotaWalletNet.Application.AccountContext.Queries.GetOutputs;
using IotaWalletNet.Application.AccountContext.Queries.GetPendingTransactions;
using IotaWalletNet.Application.AccountContext.Queries.GetTransaction;
using IotaWalletNet.Application.AccountContext.Queries.GetTransactions;
using IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs;
using IotaWalletNet.Application.Common.Builders;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Network;
using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;

namespace IotaWalletNet.Application.Common.Interfaces
{
    public interface IAccount : IRustBridgeCommunicator
    {
        string Username { get; }

        IWallet Wallet { get; }

        Task<SyncAccountResponse> SyncAccountAsync();

        Task<GetBalanceResponse> GetBalanceAsync();
        Task RequestFromFaucetAsync(string address);
        Task<GenerateAddressesResponse> GenerateAddressesAsync(uint numberOfAddresses = 1, NetworkType networkType = default);
        Task<MintNftsResponse> MintNftsAsync(List<NftOptions> nftsOptions);
        Task<GetAddressesResponse> GetAddressesAsync();
        Task<GetOutputsResponse> GetOutputsAsync(OutputFilterOptions? outputFilterOptions = null);
        Task<SendNftsResponse> SendNftsAsync(List<AddressAndNftId> addressAndNftIds);
        Task<BurnNftResponse> BurnNftAsync(string nftId);
        Task<GetUnspentOutputsResponse> GetUnspentOutputsAsync();
        Task<GetAddressesWithUnspentOutputsResponse> GetAddressesWithUnspentOutputsAsync();
        Task<GetTransactionsResponse> GetTransactionsAsync();
        Task<GetMinimumStorageDepositRequiredResponse> GetMinimumStorageDepositRequiredAsync(IOutputType outputType);
        Task<GetTransactionResponse> GetTransactionAsync(string transactionId);
        Task<GetPendingTransactionsResponse> GetPendingTransactionsAsync();
        Task<ClaimOutputsResponse> ClaimOutputsAsync(List<string> outputIds);
        Task<MintNativeTokensResponse> MintNativeTokensAsync(NativeTokenOptions nativeTokenOptions);
        Task<SendNativeTokensResponse> SendNativeTokensAsync(List<AddressWithNativeTokens> addressWithNativeTokens);
        Task<CreateAliasOutputResponse> CreateAliasOutputAsync(AliasOutputOptions aliasOutputOptions);
        Task<MeltNativeTokensResponse> MeltNativeTokensAsync(string tokenId, string meltAmountHexEncoded);
        Task<BurnNativeTokensResponse> BurnNativeTokensAsync(string tokenId, string meltAmountHexEncoded);
        Task<SendAmountResponse> SendAmountAsync(List<AddressWithAmount> addressesWithAmounts, TaggedDataPayload? taggedDataPayload = null);
        Task<ConsolidateOutputsResponse> ConsolidateOutputsAsync(bool force, int? outputsConsolidationThreshold = null);
        Task<GetFoundryOutputResponse> GetFoundryOutputAsync(string tokenId);
        Task<DestroyFoundryResponse> DestroyFoundryAsync(string foundryId);
        SendAmountBuilder SendAmountUsingBuilder();
        Task<SendMicroAmountResponse> SendMicroAmountAsync(List<AddressWithMicroAmount> addressWithMicroAmounts, TaggedDataPayload? taggedDataPayload = null);
        SendMicroAmountBuilder SendMicroAmountUsingBuilder();
    }
}
