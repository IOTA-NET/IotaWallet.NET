using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Commands.StoreMnemonic
{
    [Collection("Sequential")]
    public class StoreMnemonicTests : DependencyTestBase
    {
        [Fact]
        public async Task WalletShouldShouldStoreMnemonicOnlyTheFirstTime()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = CreateOfflineFullWallet(wallet);

            StoreMnemonicResponse storeMnemonicResponse = await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);
            storeMnemonicResponse.IsSuccess().Should().BeTrue();

            //Storing the 2nd time with SAME mnemonic is NOT ok
            storeMnemonicResponse = await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);
            storeMnemonicResponse?.IsSuccess().Should().BeFalse();

            GetNewMnemonicResponse getNewMnemonicResponse = await wallet.GetNewMnemonicAsync();

            //However storing with a DIFFERENT mnemonic is NOT ok
            storeMnemonicResponse = await wallet.StoreMnemonicAsync(getNewMnemonicResponse.Payload!);
            storeMnemonicResponse?.IsSuccess().Should().BeFalse();

        }

    }
}
