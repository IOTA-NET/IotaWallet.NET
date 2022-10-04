using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Tests.WalletContext.Queries.GetAccount
{
    [Collection("Sequential")]
    public class GetAccountTests : DependencyTestBase
    {
        [Fact]
        public async Task WalletShouldBeAbleToGetAnExistingAccount()
        {
            List<string> userNames = new List<string> { "cookiemonster", "elmo" };

            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);

            userNames.ForEach(async username => await wallet.CreateAccountAsync(username));

            (GetAccountResponse respose, IAccount? account) = await wallet.GetAccountAsync(userNames[0]);

            respose.Should().NotBeNull();
            respose.Payload.Should().NotBeNull();
            respose.Payload!.Alias.Should().Be(userNames[0]);
            
            account.Should().NotBeNull();
            account!.Username.Should().Be(userNames[0]);

            (respose, account) = await wallet.GetAccountAsync(userNames[1]);

            respose.Should().NotBeNull();
            respose.Payload.Should().NotBeNull();
            respose.Payload!.Alias.Should().Be(userNames[1]);

            account.Should().NotBeNull();
            account!.Username.Should().Be(userNames[1]);

        }
    }
}
