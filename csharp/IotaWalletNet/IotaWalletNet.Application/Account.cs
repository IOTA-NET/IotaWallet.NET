using IotaWalletNet.Application.AccountContext.Commands.BurnNft;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.AccountContext.Commands.MintNfts;
using IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet;
using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SendNfts;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetAddresses;
using IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.AccountContext.Queries.GetOutputs;
using IotaWalletNet.Application.AccountContext.Queries.GetTransactions;
using IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Network;
using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using static IotaWalletNet.Application.AccountContext.Commands.SyncAccount.SyncAccountCommandHandler;

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

        public async Task RequestFromFaucetAsync(string address, string url)
        {
            await _mediator.Send(new RequestFromFaucetCommand(address, url));
        }


        public async Task<MintNftsResponse> MintNftsAsync(List<NftOptions> nftsOptions)
        {
            return await _mediator.Send(new MintNftsCommand(this, Username, nftsOptions));
        }

        public async Task<GetBalanceResponse> GetBalanceAsync()
        {
            return await _mediator.Send(new GetBalanceQuery(this, Username));
        }

        public async Task<string> SendAmountAsync(AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions)
        {
            return await _mediator.Send(new SendAmountCommand(this, Username, addressesWithAmountAndTransactionOptions));
        }

        public async Task<SyncAccountResponse> SyncAccountAsync()
        {
            return await _mediator.Send(new SyncAccountCommand(this, Username));
        }
    }
}
