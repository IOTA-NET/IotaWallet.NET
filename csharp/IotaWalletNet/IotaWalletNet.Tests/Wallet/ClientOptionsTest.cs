using FluentAssertions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Wallet
{
    [Collection("Sequential")]
    public class ClientOptionsTest : DependencyTestBase
    {

        [Fact]
        public void ClientOptionsBuilderShouldReturnWalletWhenBuild()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = wallet
                        .ConfigureClientOptions()
                        .IsLocalPow()
                        .IsFallbackToLocalPow()
                        .AddNodeUrl(DEFAULT_API_URL)
                        .Then();

            wallet.Should().NotBeNull();


        }

        [Fact]
        public void AddingDuplicateNodesShouldBeTreatedAsSingle()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            string nodeUrl = "www.test.com";

            wallet = wallet
                    .ConfigureClientOptions()
                        .IsLocalPow()
                        .IsFallbackToLocalPow()
                        .AddNodeUrl(nodeUrl)
                        .AddNodeUrl(nodeUrl)
                        .Then();

            ClientOptions clientOptions = wallet.GetWalletOptions().ClientConfigOptions;

            clientOptions.Nodes.Should().HaveCount(1);
            clientOptions.Nodes.First().Equals(nodeUrl);


        }

        [Fact]
        public void AddingNonDuplicateNodesShouldBeTreatedAsMultiple()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            string nodeUrl = "www.test.com";
            string secondNodeUrl = "www.hello.com";

            wallet = wallet
                    .ConfigureClientOptions()
                        .IsLocalPow()
                        .IsFallbackToLocalPow()
                        .AddNodeUrl(nodeUrl)
                        .AddNodeUrl(secondNodeUrl)
                        .Then();

            ClientOptions clientOptions = wallet.GetWalletOptions().ClientConfigOptions;

            clientOptions.Nodes.Should().HaveCount(2);
            clientOptions.Nodes.First().Equals(nodeUrl);
            clientOptions.Nodes.ToList()[1].Equals(secondNodeUrl);


        }

        [Fact]
        public void ClientOptionsShouldBeConfigurableByBuilder()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();
            string nodeUrl = "www.test.com";

            wallet = wallet
                    .ConfigureClientOptions()
                        .IsLocalPow()
                        .IsFallbackToLocalPow()
                        .AddNodeUrl(nodeUrl)
                        .Then();


            ClientOptions clientOptions = wallet.GetWalletOptions().ClientConfigOptions;

            clientOptions.LocalPow.Should().BeTrue();
            clientOptions.FallbackToLocalPow.Should().BeTrue();
            clientOptions.Nodes.Should().HaveCount(1);
            clientOptions.Nodes.First().Equals(nodeUrl);

            wallet = wallet
                    .ConfigureClientOptions()
                        .IsLocalPow(false)
                        .IsFallbackToLocalPow(false)
                        .AddNodeUrl(nodeUrl)
                        .Then();

            clientOptions = wallet.GetWalletOptions().ClientConfigOptions;

            clientOptions.LocalPow.Should().BeFalse();
            clientOptions.FallbackToLocalPow.Should().BeFalse();
            clientOptions.Nodes.Should().HaveCount(1);
            clientOptions.Nodes.First().Equals(nodeUrl);


        }
    }
}