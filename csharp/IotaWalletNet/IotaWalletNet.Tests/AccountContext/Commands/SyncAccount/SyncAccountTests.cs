using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using static IotaWalletNet.Application.AccountContext.Commands.SyncAccount.SyncAccountCommandHandler;

namespace IotaWalletNet.Tests.AccountContext.Commands.SyncAccount
{
    [Collection("Sequential")]
    public class SyncAccountTests : DependencyTestBase
    {
        [Fact]
        public async Task ValidAccountShouldBeAbleToBeSynced()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);

            string username = "cookiemonster";

            await wallet.CreateAccountAsync(username);

            (_, IAccount? account) = await wallet.GetAccountAsync(username);

            SyncAccountResponse response = await account!.SyncAccountAsync();

            response.Should().NotBeNull();
            response.IsSuccess().Should().BeTrue();
        }
    }
}
