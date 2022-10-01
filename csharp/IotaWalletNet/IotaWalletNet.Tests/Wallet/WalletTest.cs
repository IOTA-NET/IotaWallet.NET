using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using static IotaWalletNet.Application.Common.Options.WalletOptions;

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
                            .SetCoinType(WalletOptions.TypeOfCoin.Shimmer)
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
