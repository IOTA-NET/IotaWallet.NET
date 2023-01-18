using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.AccountContext.Commands.RequestFromFaucet
{

    [Collection("Sequential")]
    public class SendAmountTests : DependencyTestBase, IDisposable
    {
        [Fact]
        public async Task AccountShouldBeAbleTSendTokens()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = await CreateFullWalletAsync(wallet);

            (_, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");


            string address = (await account.GetAddressesAsync()).Payload!.First().Address;

            await account.RequestFromFaucetAsync(address);

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_FAUCET));

            long oldBalance = await GetBalanceAsync(account);

            oldBalance.Should().Be(1000_000_000); //given by faucet, 1000 SMR

            oldBalance = await TestSendAmountByUsingASingleAmount(oldBalance, account);

            oldBalance = await TestSendAmountByUsingMultipleAmounts(oldBalance, account);

            oldBalance = await TestSendAmountBuilderByUsingASingleAmount(oldBalance, account);

            oldBalance = await TestSendAmountBuilderByUsingMultipleAmounts(oldBalance, account);
        }

        private async Task<long> TestSendAmountByUsingASingleAmount(long oldBalance, IAccount account)
        {
            List<AddressWithAmount> addressWithAmounts = new List<AddressWithAmount>()
            {
                new AddressWithAmount(ANOTHER_WALLET_ADDRESS, "1000000")
            };

            await account.SendAmountAsync(addressWithAmounts);

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_TRANSACTION));

            long newBalance = await GetBalanceAsync(account);

            long difference = oldBalance - newBalance;

            difference.Should().Be(1_000_000);

            return newBalance;
        }

        private async Task<long> TestSendAmountByUsingMultipleAmounts(long oldBalance, IAccount account)
        {
            List<AddressWithAmount> addressWithAmounts = new List<AddressWithAmount>()
            {
                new AddressWithAmount(ANOTHER_WALLET_ADDRESS, "1000000"),
                new AddressWithAmount(ANOTHER_WALLET_ADDRESS, "1000000")

            };

            await account.SendAmountAsync(addressWithAmounts);

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_TRANSACTION));

            long newBalance = await GetBalanceAsync(account);

            long difference = oldBalance - newBalance;

            difference.Should().Be(2_000_000);

            return newBalance;

        }


        private async Task<long> TestSendAmountBuilderByUsingASingleAmount(long oldBalance, IAccount account)
        {
            await account.SendAmountUsingBuilder()
                            .AddAddressAndAmount(ANOTHER_WALLET_ADDRESS, 1_000_000)
                            .SendAmountAsync();

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_TRANSACTION));

            long newBalance = await GetBalanceAsync(account);

            long difference = oldBalance - newBalance;

            difference.Should().Be(1_000_000);

            return newBalance;
        }

        private async Task<long> TestSendAmountBuilderByUsingMultipleAmounts(long oldBalance, IAccount account)
        {
            await account.SendAmountUsingBuilder()
                            .AddAddressAndAmount(ANOTHER_WALLET_ADDRESS, 1_000_000)
                            .AddAddressAndAmount(ANOTHER_WALLET_ADDRESS, 1_000_000)
                            .SendAmountAsync();

            await Task.Delay(TimeSpan.FromSeconds(SLEEP_DURATION_SECONDS_TRANSACTION));

            long newBalance = await GetBalanceAsync(account);

            long difference = oldBalance - newBalance;

            difference.Should().Be(2000000);

            return newBalance;

        }


        private async Task<long> GetBalanceAsync(IAccount account)
        {
            await account.SyncAccountAsync();

            GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();

            long balance = long.Parse(getBalanceResponse.Payload!.BaseCoin.Total);

            return balance;
        }

        

    }
}
