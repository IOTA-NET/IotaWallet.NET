using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.WalletContext.Queries.GetNewMnemonic
{
    [Collection("Sequential")]
    public class GetNewMnemonicTests : DependencyTestBase
    {
        [Fact]
        public async Task WalletShouldBeAbleToCreateAMnemonic()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            GetNewMnemonicResponse? getNewMnemonicResponse = null;

            await wallet.Invoking(async w => getNewMnemonicResponse = await w.GetNewMnemonicAsync())
                        .Should()
                        .NotThrowAsync();

            string? mnemonic = getNewMnemonicResponse?.Payload;

            mnemonic.Should().NotBeNullOrEmpty();

            mnemonic!.Trim().Split().Should().HaveCount(24);

        }
    }
}
