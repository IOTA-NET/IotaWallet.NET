using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.AccountContext.Commands.RequestFromFaucet
{

    [Collection("Sequential")]
    public class RequestFromFaucetTests : DependencyTestBase, IDisposable
    {
        [Fact]
        public async Task AccountShouldBeAbleToGetTokensFromFaucet()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = await CreateFullWalletAsync(wallet);

            (_, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");

            GenerateAddressesResponse generateAddressesResponse
                = await account!.GenerateAddressesAsync();

            string address = generateAddressesResponse.Payload?.First()?.Address!;

            await account.RequestFromFaucetAsync(address, DEFAULT_FAUCET_URL);

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_FAUCET));

            await account.SyncAccountAsync();
            var getBalanceResponse = await account.GetBalanceAsync();

            long newBalance = long.Parse(getBalanceResponse.Payload!.BaseCoin.Total);

            newBalance.Should().BeGreaterThan(0);
        }
    }
}
