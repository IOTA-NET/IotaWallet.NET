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
                            .SetSnapshotPath(GetRandomStrongholdFilename())
                            .SetPassword("password")
                            .Then();

            wallet.Should().NotBeNull();


        }

        [Fact]
        public void SecretManagerOptionShouldBeConfigurableByBuilder()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            string password = "password";

            string filename = GetRandomStrongholdFilename();
            wallet = wallet
                        .ConfigureSecretManagerOptions()
                            .SetSnapshotPath(filename)
                            .SetPassword(password)
                            .Then();

            SecretManagerOptions secretManagerOptions = wallet.GetWalletOptions().SecretManager;

            secretManagerOptions.Stronghold.SnapshotPath.Equals(filename);
            secretManagerOptions.Stronghold.Password.Equals(password);


        }
    }
}
