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

            await account.SyncAccountAsync();
            GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
            long totalBalance = long.Parse(getBalanceResponse.Payload!.BaseCoin.Total);

            long thousandShimmer = 1000000 * 1000;
            if (totalBalance >= thousandShimmer)
            {
                long amountToSend = totalBalance - thousandShimmer;

                //Lets send our tokens to some other address, else we cant ask from faucet
                var transactionOptions = new AddressesWithAmountAndTransactionOptions().AddAddressAndAmount(ANOTHER_WALLET_ADDRESS, amountToSend.ToString());
                await account.SendAmountAsync(transactionOptions);

                await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_TRANSACTION));//Let's wait for it to be confirmed
                await account.SyncAccountAsync();

                getBalanceResponse = await account.GetBalanceAsync();
                totalBalance = long.Parse(getBalanceResponse.Payload!.BaseCoin.Total);

                if (totalBalance > thousandShimmer)
                    throw new Exception("Tried sending out shimmer in order to obtain new ones from faucet, however, sending out shimmer failed or did not complete on time.");
            }

            //await account.RequestFromFaucetAsync(address, DEFAULT_FAUCET_URL);

            //await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_FAUCET));

            //await account.SyncAccountAsync();

            //This will create a basic output
            await account.SendAmountAsync(new AddressesWithAmountAndTransactionOptions().AddAddressAndAmount(ANOTHER_WALLET_ADDRESS, "1000000"));

            NFTIRC27 nftIrc27 = new NFTIRC27(KnownMimeTypes.Text, "IotaWalletNetTest", "iotawallet.net");

            NftOptions nftOptions = new NftOptions() { ImmutableMetadata = JsonConvert.SerializeObject(nftIrc27).ToHexString() };

            MintNftsResponse mintNftsResponse = await account.MintNftsAsync(new NftOptions[] { nftOptions }.ToList());

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_TRANSACTION));

            await account.SyncAccountAsync();

            GetOutputsResponse response = await account.GetOutputsAsync();

            response.Should().NotBeNull();
            response.IsSuccess().Should().BeTrue();
            response.Payload!.Count.Should().BeGreaterThanOrEqualTo(2);
        }
    }
}
