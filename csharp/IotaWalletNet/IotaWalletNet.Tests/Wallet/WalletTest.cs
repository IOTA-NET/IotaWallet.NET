using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Wallet
{
    [Collection("Sequential")]
    public class WalletTest : DependencyTestBase
    {

        [Fact]
        public void WalletShouldBeInitializedWithBasicConfigurations()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = wallet
                        .ConfigureSecretManagerOptions()
                            .SetPassword("password")
                            .ThenBuild()
                        .ConfigureClientOptions()
                            .AddNodeUrl("https://api.testnet.shimmer.network")
                            .ThenBuild()
                        .ConfigureWalletOptions()
                            .SetCoinType(TypeOfCoin.Shimmer)
                            .ThenBuild();

            wallet
                .Invoking(w => w = w.ThenInitialize())
                .Should()
                .NotThrow();


        }

        [Fact]
        public void WalletShouldBeInitializedWithAdvancedConfigurations()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = wallet
                        .ConfigureWalletOptions()
                            .SetCoinType(TypeOfCoin.Shimmer)
                            .SetStoragePath(DATABASE_PATH)
                            .ThenBuild()
                        .ConfigureClientOptions()
                            .AddNodeUrl("https://api.testnet.shimmer.network")
                            .IsFallbackToLocalPow()
                            .IsLocalPow()
                            .ThenBuild()
                        .ConfigureSecretManagerOptions()
                            .SetPassword("password")
                            .SetSnapshotPath(STRONGHOLD_PATH)
                            .ThenBuild();

            wallet
                .Invoking(w => w = w.ThenInitialize())
                .Should()
                .NotThrow();


        }
    }
}
