using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Commands.CreateAccount
{
    [Collection("Sequential")]
    public class CreateAccountTests : DependencyTestBase
    {
        [Fact]
        public async Task WalletShouldBeAbleToCreateAccountsWithDifferentUserNames ()
        {
            List<string> userNames = new List<string> { "cookiemonster", "elmo" };

            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);

            foreach (string username in userNames)
            {
                (CreateAccountResponse r, IAccount? account) = await wallet.CreateAccountAsync(username);

                account.Should().NotBeNull();
                account!.Username.Should().Be(username);

                r.Should().NotBeNull();
                r.IsSuccess().Should().BeTrue();
                r!.Payload.Should().NotBeNull();
                r!.Payload!.Alias.Should().Be(username);
            }
        }
    }
}
