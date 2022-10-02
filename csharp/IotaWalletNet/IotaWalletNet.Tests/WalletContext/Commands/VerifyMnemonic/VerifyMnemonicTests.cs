using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Commands.VerifyMnemonic
{
    [Collection("Sequential")]
    public class VerifyMnemonicTests : DependencyTestBase
    {
        [Fact]
        public async Task WalletShouldValidateTrueForCorrectMnemonic()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = CreateOfflineFullWallet(wallet);

            VerifyMnemonicResponse response = await wallet.VerifyMnemonicAsync(DEFAULT_MNEMONIC);
            response.Should().NotBeNull();
            response.Error.Should().BeNull();
            response.Type.Should().NotBeNullOrEmpty();
                        

        }

        [Fact]
        public async Task WalletShouldValidateFalseForWrongMnemonic()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = CreateOfflineFullWallet(wallet);

            VerifyMnemonicResponse response = await wallet.VerifyMnemonicAsync("wrong mnemonic");
            response.Should().NotBeNull();
            response.Error.Should().NotBeNull();
            response.Type.Should().NotBeNullOrEmpty();

            response = await wallet.VerifyMnemonicAsync(DEFAULT_MNEMONIC + "random");
            response.Should().NotBeNull();
            response.Error.Should().NotBeNull();
            response.Type.Should().NotBeNullOrEmpty();

            string wrongMnemonic = "symbol sail venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";

            response = await wallet.VerifyMnemonicAsync(wrongMnemonic);
            response.Should().NotBeNull();
            response.Error.Should().NotBeNull();
            response.Type.Should().NotBeNullOrEmpty();

        }
    }
}
