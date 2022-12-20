using FluentAssertions;
using IotaWalletNet.Application.Common.Exceptions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Commands.StoreMnemonic
{
    [Collection("Sequential")]
    public class StoreMnemonicTests : DependencyTestBase, IDisposable
    {
        [Fact]
        public async Task WalletShouldShouldStoreMnemonicOnlyTheFirstTime()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = await CreateOfflineFullWalletAsync(wallet, shouldCreateAndStoreMnemonic: false);
            string mnemonic = "hood medal among prevent during embrace inmate swarm ancient damp token rail wolf risk tortoise record dose language rival cloud sting grace palm style";
            StoreMnemonicResponse storeMnemonicResponse = await wallet.StoreMnemonicAsync(mnemonic);
            storeMnemonicResponse.IsSuccess().Should().BeTrue();

            //Storing the 2nd time with SAME mnemonic is NOT ok
            await wallet.Awaiting(x => x.StoreMnemonicAsync(mnemonic)).Should().ThrowAsync<RustBridgeException>();


            GetNewMnemonicResponse getNewMnemonicResponse = await wallet.GetNewMnemonicAsync();

            //And storing with a DIFFERENT mnemonic is also NOT ok
            await wallet.Awaiting(x => x.StoreMnemonicAsync(getNewMnemonicResponse.Payload!)).Should().ThrowAsync<RustBridgeException>();


        }

    }
}
