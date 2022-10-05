using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Queries.GetAccounts;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Queries.GetAccounts
{
    [Collection("Sequential")]
    public class GetAccountsTests : DependencyTestBase
    {
        [Fact]
        public async Task WalletShouldBeAbleToGetAllCreatedAccounts()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);


            await wallet.CreateAccountAsync("cookiemonster");
            await wallet.CreateAccountAsync("elmo");

            GetAccountsResponse getAccountsResponse = await wallet.GetAccountsAsync();
            getAccountsResponse.Should().NotBeNull();
            getAccountsResponse.Payload!.Count.Should().Be(2);
        }
    }
}
