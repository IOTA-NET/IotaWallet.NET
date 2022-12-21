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
                            .Then()
                        .ConfigureClientOptions()
                            .AddNodeUrl("https://api.testnet.shimmer.network")
                            .SetFaucetUrl("https://faucet.testnet.shimmer.network")
                            .Then()
                        .ConfigureWalletOptions()
                            .SetCoinType(TypeOfCoin.Shimmer)
                            .Then();

            wallet
                .Invoking(w => w = w.Initialize())
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
                            .SetStoragePath(GetRandomDatabaseFilename())
                            .Then()
                        .ConfigureClientOptions()
                            .AddNodeUrl("https://api.testnet.shimmer.network")
                            .SetFaucetUrl("https://faucet.testnet.shimmer.network")
                            .IsFallbackToLocalPow()
                            .IsLocalPow()
                            .Then()
                        .ConfigureSecretManagerOptions()
                            .SetPassword("password")
                            .SetSnapshotPath(GetRandomStrongholdFilename())
                            .Then();

            wallet
                .Invoking(w => w = w.Initialize())
                .Should()
                .NotThrow();


        }
    }
}
