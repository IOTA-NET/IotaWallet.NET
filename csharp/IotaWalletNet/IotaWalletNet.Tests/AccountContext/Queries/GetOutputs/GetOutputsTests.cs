using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.AccountContext.Commands.MintNfts;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.AccountContext.Queries.GetOutputs;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MimeMapping;
using Newtonsoft.Json;

namespace IotaWalletNet.Tests.AccountContext.Queries.GetOutputs
{
    [Collection("Sequential")]
    public class GetOutputsTests : DependencyTestBase
    {
        [Fact]
        public async Task AccountShouldBeAbleToGetOutputsAfterSyncing()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);

            (_, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");

            GenerateAddressesResponse generateAddressesResponse
                = await account!.GenerateAddressesAsync();

            string address = generateAddressesResponse.Payload?.First()?.Address!;

            await account.RequestFromFaucetAsync(address, DEFAULT_FAUCET_URL);

            Thread.Sleep(30 * 1000);

            await account.SyncAccountAsync();

            //This will create a basic output
            await account.SendAmountAsync(new AddressesWithAmountAndTransactionOptions().AddAddressAndAmount(ANOTHER_WALLET_ADDRESS, "1000000"));

            NFTIRC27 nftIrc27 = new NFTIRC27(KnownMimeTypes.Text, "IotaWalletNetTest", "iotawallet.net");

            NftOptions nftOptions = new NftOptions() { ImmutableMetadata = JsonConvert.SerializeObject(nftIrc27).ToHexString() };

            MintNftsResponse mintNftsResponse = await account.MintNftsAsync(new NftOptions[] { nftOptions }.ToList());

            Thread.Sleep(20 * 1000);

            GetOutputsResponse response = await account.GetOutputsAsync();

            response.Should().NotBeNull();
            response.IsSuccess().Should().BeTrue();
            response.Payload!.Count.Should().BeGreaterThanOrEqualTo(2);
        }
    }
}
