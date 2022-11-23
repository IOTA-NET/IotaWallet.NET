using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.AccountContext.Commands.SyncAccount
{
    [Collection("Sequential")]
    public class SyncAccountTests : DependencyTestBase, IDisposable
    {
        [Fact]
        public async Task ValidAccountShouldBeAbleToBeSynced()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = await CreateFullWalletAsync(wallet);

            string username = "cookiemonster";

            await wallet.CreateAccountAsync(username);

            (_, IAccount? account) = await wallet.GetAccountAsync(username);

            SyncAccountResponse response = await account!.SyncAccountAsync();

            response.Should().NotBeNull();
            response.IsSuccess().Should().BeTrue();
        }
    }
}
