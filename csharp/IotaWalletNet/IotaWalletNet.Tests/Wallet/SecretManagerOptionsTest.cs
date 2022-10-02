using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Wallet
{
    [Collection("Sequential")]
    public class SecretManagerOptionsTest : DependencyTestBase
    {
        [Fact]
        public void SecretManagerOptionBuilderShouldReturnWalletWhenBuild()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            wallet = wallet
                        .ConfigureSecretManagerOptions()
                            .SetSnapshotPath(STRONGHOLD_PATH)
                            .SetPassword("password")
                            .ThenBuild();

            wallet.Should().NotBeNull();


        }

        [Fact]
        public void SecretManagerOptionShouldBeConfigurableByBuilder()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            string password = "password";

            wallet = wallet
                        .ConfigureSecretManagerOptions()
                            .SetSnapshotPath(STRONGHOLD_PATH)
                            .SetPassword(password)
                            .ThenBuild();

            SecretManagerOptions secretManagerOptions = wallet.GetWalletOptions().SecretManager;

            secretManagerOptions.Stronghold.SnapshotPath.Equals(STRONGHOLD_PATH);
            secretManagerOptions.Stronghold.Password.Equals(password);


        }
    }
}
