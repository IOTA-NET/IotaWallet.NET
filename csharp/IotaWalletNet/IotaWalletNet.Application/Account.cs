﻿using IotaWalletNet.Application.AccountContext.Commands.BuildBasicOutput;
using IotaWalletNet.Application.AccountContext.Commands.BuildNftOutput;
using IotaWalletNet.Application.AccountContext.Commands.BurnNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.BurnNft;
using IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs;
using IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs;
using IotaWalletNet.Application.AccountContext.Commands.CreateAliasOutput;
using IotaWalletNet.Application.AccountContext.Commands.DestroyFoundry;
using IotaWalletNet.Application.AccountContext.Commands.EnablePeriodicSyncing;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.MintNfts;
using IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet;
using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount;
using IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.SendNfts;
using IotaWalletNet.Application.AccountContext.Commands.SendOutputs;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetAddresses;
using IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.AccountContext.Queries.GetFoundryOutput;
using IotaWalletNet.Application.AccountContext.Queries.GetMinimumStorageDepositRequired;
using IotaWalletNet.Application.AccountContext.Queries.GetOutputs;
using IotaWalletNet.Application.AccountContext.Queries.GetOutputsWithAdditionalUnlockConditions;
using IotaWalletNet.Application.AccountContext.Queries.GetPendingTransactions;
using IotaWalletNet.Application.AccountContext.Queries.GetTransaction;
using IotaWalletNet.Application.AccountContext.Queries.GetTransactions;
using IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs;
using IotaWalletNet.Application.Common.Builders;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Network;
using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;

namespace IotaWalletNet.Application
{
    public class Account : RustBridgeCommunicator, IAccount
    {
        private readonly IMediator _mediator;

        public Account(IMediator mediator, string username, IWallet wallet)
            : base(wallet.GetWalletHandle())
        {
            _mediator = mediator;
            Username = username;
            Wallet = wallet;
        }

        public string Username { get; }
        public IWallet Wallet { get; }


        public async Task<T> RetryAsyncFuncUntil<T>(Func<Task<T>> function, int intervalInMilliseconds, Func<T, bool> predicate, CancellationToken cancellationToken=default)
        {
            while(true)
            {
                T response = await function();

                if(predicate(response))
                {
                    return response;
                }

                if (cancellationToken.IsCancellationRequested)
                    return response;
                
                await Task.Delay(intervalInMilliseconds);

                if (cancellationToken.IsCancellationRequested)
                    return response;
            }
        }

        public async Task<SendOutputsResponse> SendOutputsAsync(List<IOutputType> outputs, TaggedDataPayload? taggedDataPayload = null)
        {
            return await _mediator.Send(new SendOutputsCommand(this, Username, outputs, taggedDataPayload));
        }

        public async Task<BuildBasicOutputResponse> BuildBasicOutputAsync(BuildBasicOutputData buildBasicOutputData)
        {
            return await _mediator.Send(new BuildBasicOutputCommand(buildBasicOutputData, Username, this));
        }

        public async Task<BuildNftOutputResponse> BuildNftOutputAsync(BuildNftOutputData buildNftOutputData)
        {
            return await _mediator.Send(new BuildNftOutputCommand(buildNftOutputData, Username, this));
        }

        public async Task EnablePeriodicSyncing(int intervalInMilliSeconds, CancellationToken cancellationToken=default)
        {
            await _mediator.Send(new EnablePeriodicSyncingCommand(this, intervalInMilliSeconds), cancellationToken);
        }

        public async Task<GetOutputsWithAdditionalUnlockConditionsResponse> GetOutputsWithAdditionalUnlockConditionsAsync(OutputTypeToClaim outputTypeToClaim)
        {
            return await _mediator.Send(new GetOutputsWithAdditionalUnlockConditionsQuery(outputTypeToClaim, Username, this));
        }

        public async Task<SendMicroAmountResponse> SendMicroAmountAsync(List<AddressWithMicroAmount> addressWithMicroAmounts, TaggedDataPayload? taggedDataPayload = null)
        {
            return await _mediator.Send(new SendMicroAmountCommand(addressWithMicroAmounts, taggedDataPayload, Username, this));
        }

        public async Task<DestroyFoundryResponse> DestroyFoundryAsync(string foundryId)
        {
            return await _mediator.Send(new DestroyFoundryCommand(foundryId, Username, this));
        }

        public async Task<ConsolidateOutputsResponse> ConsolidateOutputsAsync(bool force, int? outputsConsolidationThreshold = null)
        {
            return await _mediator.Send(new ConsolidateOutputsCommand(force, Username, this, outputsConsolidationThreshold));
        }

        public async Task<GetFoundryOutputResponse> GetFoundryOutputAsync(string tokenId)
        {
            return await _mediator.Send(new GetFoundryOutputQuery(tokenId, Username, this));
        }

        public async Task<CreateAliasOutputResponse> CreateAliasOutputAsync(AliasOutputOptions aliasOutputOptions)
        {
            return await _mediator.Send(new CreateAliasOutputCommand(Username, this, aliasOutputOptions));
        }

        public async Task<GetTransactionResponse> GetTransactionAsync(string transactionId)
        {
            return await _mediator.Send(new GetTransactionQuery(Username, this, transactionId));
        }

        public async Task<MeltNativeTokensResponse> MeltNativeTokensAsync(string tokenId, string meltAmountHexEncoded)
        {
            return await _mediator.Send(new MeltNativeTokensCommand(tokenId, meltAmountHexEncoded, Username, this));
        }

        public async Task<BurnNativeTokensResponse> BurnNativeTokensAsync(string tokenId, string burnAmountHexEncoded)
        {
            return await _mediator.Send(new BurnNativeTokensCommand(tokenId, burnAmountHexEncoded, Username, this));
        }
        public async Task<SendNativeTokensResponse> SendNativeTokensAsync(List<AddressWithNativeTokens> addressWithNativeTokens)
        {
            return await _mediator.Send(new SendNativeTokensCommand(Username, this, addressWithNativeTokens));
        }
        public async Task<MintNativeTokensResponse> MintNativeTokensAsync(NativeTokenOptions nativeTokenOptions)
        {
            return await _mediator.Send(new MintNativeTokensCommand(Username, this, nativeTokenOptions));
        }

        public async Task<GetPendingTransactionsResponse> GetPendingTransactionsAsync()
        {
            return await _mediator.Send(new GetPendingTransactionsQuery(Username, this));
        }

        public async Task<ClaimOutputsResponse> ClaimOutputsAsync(List<string> outputIds)
        {
            return await _mediator.Send(new ClaimOutputsCommand(Username, this, outputIds));
        }

        public async Task<GetMinimumStorageDepositRequiredResponse> GetMinimumStorageDepositRequiredAsync(IOutputType outputType)
        {
            return await _mediator.Send(new GetMinimumStorageDepositRequiredQuery(Username, this, outputType));
        }

        public async Task<GetTransactionsResponse> GetTransactionsAsync()
        {
            return await _mediator.Send(new GetTransactionsQuery(Username, this));
        }
        public async Task<GetAddressesWithUnspentOutputsResponse> GetAddressesWithUnspentOutputsAsync()
        {
            return await _mediator.Send(new GetAddressesWithUnspentOutputsQuery(Username, this));
        }

        public async Task<SendNftsResponse> SendNftsAsync(List<AddressAndNftId> addressAndNftIds)
        {
            return await _mediator.Send(new SendNftsCommand(Username, this, addressAndNftIds));
        }

        public async Task<GetUnspentOutputsResponse> GetUnspentOutputsAsync()
        {
            return await _mediator.Send(new GetUnspentOutputsQuery(Username, this));
        }

        public async Task<BurnNftResponse> BurnNftAsync(string nftId)
        {
            return await _mediator.Send(new BurnNftCommand(nftId, Username, this));
        }

        public async Task<GetOutputsResponse> GetOutputsAsync(OutputFilterOptions? outputFilterOptions = null)
        {
            return await _mediator.Send(new GetOutputsQuery(Username, this, outputFilterOptions));
        }

        public async Task<GenerateAddressesResponse> GenerateAddressesAsync(uint numberOfAddresses, NetworkType networkType)
        {
            return await _mediator.Send(new GenerateAddressesCommand(this, networkType, Username, numberOfAddresses));
        }

        public async Task<GetAddressesResponse> GetAddressesAsync()
        {
            return await _mediator.Send(new GetAddressesQuery(Username, this));
        }

        public async Task RequestFromFaucetAsync(string address)
        {
            string faucetUrl = Wallet.GetWalletOptions().ClientConfigOptions.FaucetUrl;
            await _mediator.Send(new RequestFromFaucetCommand(address, faucetUrl));
        }


        public async Task<MintNftsResponse> MintNftsAsync(List<NftOptions> nftsOptions)
        {
            return await _mediator.Send(new MintNftsCommand(this, Username, nftsOptions));
        }

        public async Task<GetBalanceResponse> GetBalanceAsync()
        {
            return await _mediator.Send(new GetBalanceQuery(this, Username));
        }

        public async Task<SendAmountResponse> SendAmountAsync(List<AddressWithAmount> addressesWithAmounts, TaggedDataPayload? taggedDataPayload = null)
        {
            return await _mediator.Send(new SendAmountCommand(this, Username, addressesWithAmounts, taggedDataPayload));
        }

        public SendAmountBuilder SendAmountUsingBuilder() => new SendAmountBuilder(_mediator, this, Username);

        public SendMicroAmountBuilder SendMicroAmountUsingBuilder() => new SendMicroAmountBuilder(_mediator, this, Username);

        public async Task<SyncAccountResponse> SyncAccountAsync()
        {
            return await _mediator.Send(new SyncAccountCommand(this, Username));
        }
    }
}
