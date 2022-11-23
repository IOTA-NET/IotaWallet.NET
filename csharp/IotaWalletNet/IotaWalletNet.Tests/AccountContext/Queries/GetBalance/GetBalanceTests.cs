using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.AccountContext.Queries.GetBalance
{
    [Collection("Sequential")]
    public class GetBalanceTests : DependencyTestBase, IDisposable
    {
        [Fact]
        public async Task AccountShouldBeAbleToGetBalanceAfterSync()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = await CreateFullWalletAsync(wallet);


            (_, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");

            await account!.SyncAccountAsync();
            GetBalanceResponse response = await account.GetBalanceAsync();

            response.Should().NotBeNull();
            response.Error.Should().BeNull();
            response.Payload.Should().NotBeNull();

        }
    }
}
