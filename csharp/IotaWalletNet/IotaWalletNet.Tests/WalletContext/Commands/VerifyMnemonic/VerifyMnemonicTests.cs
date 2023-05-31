using FluentAssertions;
using IotaWalletNet.Application.Common.Exceptions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Commands.VerifyMnemonic
{
    [Collection("Sequential")]
    public class VerifyMnemonicTests : DependencyTestBase, IDisposable
    {
        [Fact]
        public async Task WalletShouldValidateTrueForCorrectMnemonic()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = await CreateOfflineFullWalletAsync(wallet, shouldCreateAndStoreMnemonic: false);
            string mnemonic = "hood medal among prevent during embrace inmate swarm ancient damp token rail wolf risk tortoise record dose language rival cloud sting grace palm style";

            VerifyMnemonicResponse response = await wallet.VerifyMnemonicAsync(mnemonic);
            response.Should().NotBeNull();
            response.Error.Should().BeNull();
            response.Type.Should().NotBeNullOrEmpty();


        }

        [Fact]
        public async Task WalletShouldValidateFalseForWrongMnemonic()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = await CreateOfflineFullWalletAsync(wallet, shouldCreateAndStoreMnemonic: false);
            string mnemonic = "hood medal among prevent during embrace inmate swarm ancient damp token rail wolf risk tortoise record dose language rival cloud sting grace palm style";
            try
            {
            await wallet.VerifyMnemonicAsync("asdasd");

            }
            catch(RustBridgeException e)
            {

            }

            await wallet.Awaiting(x => x.VerifyMnemonicAsync("wrong mnemonic")).Should().ThrowAsync<RustBridgeException>();
            await wallet.Awaiting(x => x.VerifyMnemonicAsync(mnemonic + "random")).Should().ThrowAsync<RustBridgeException>();

            string wrongMnemonic = "symbol sail venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";

            await wallet.Awaiting(x => x.VerifyMnemonicAsync(wrongMnemonic)).Should().ThrowAsync<RustBridgeException>();


        }
    }
}
