using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Wallet
{
    public class SecretManagerOptionsTest : DependencyTestBase
    {
        [Fact]
        public void SecretManagerOptionBuilderShouldReturnWalletWhenBuild()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = wallet
                        .ConfigureSecretManagerOptions()
                            .SetSnapshotPath("./snapshot")
                            .SetPassword("password")
                            .ThenBuild();

            wallet.Should().NotBeNull();
        }

        [Fact]
        public void SecretManagerOptionShouldBeConfigurableByBuilder()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            string snapshotPath = "./snapshot";
            string password = "password";

            wallet = wallet
                        .ConfigureSecretManagerOptions()
                            .SetSnapshotPath(snapshotPath)
                            .SetPassword(password)
                            .ThenBuild();

            SecretManagerOptions secretManagerOptions = wallet.GetWalletOptions().SecretManager;

            secretManagerOptions.Stronghold.SnapshotPath.Equals(snapshotPath);
            secretManagerOptions.Stronghold.Password.Equals(password);

        }
    }
}
