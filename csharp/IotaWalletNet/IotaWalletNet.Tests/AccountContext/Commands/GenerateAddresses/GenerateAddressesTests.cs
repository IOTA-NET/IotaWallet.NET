using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.AccountContext.Commands.GenerateAddresses
{
    [Collection("Sequential")]
    public class GenerateAddressesTests : DependencyTestBase
    {
        [Fact]
        public async Task AccountShouldBeAbleToGenerateNewAddresses()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);

            (CreateAccountResponse accountResponse, IAccount? account) =
                await wallet.CreateAccountAsync("cookiemonster");

            GenerateAddressesResponse response = await account!.GenerateAddressesAsync(numberOfAddresses: 1);

            response.Should().NotBeNull();
            response.IsSuccess().Should().BeTrue();
            response.Payload.Should().NotBeNull();
            response.Payload!.Count.Should().Be(1);
            response.Payload.First().Internal.Should().BeFalse();
            response.Payload!.First().Used.Should().BeFalse();
            response.Payload!.First().Address.Should().NotBeNullOrEmpty();

            response = await account!.GenerateAddressesAsync(numberOfAddresses: 2);

            response.Should().NotBeNull();
            response.IsSuccess().Should().BeTrue();
            response.Payload.Should().NotBeNull();
            response.Payload!.Count.Should().Be(2);

            response.Payload.ForEach(payload =>
            {
                payload.Internal.Should().BeFalse();
                payload.Used.Should().BeFalse();
                payload.Address.Should().NotBeNullOrEmpty();

            });
        }
    }
}
