using FluentAssertions;
using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Tests.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.AccountContext.Commands.RequestFromFaucet
{

    [Collection("Sequential")]
    public class RequestFromFaucetTests : DependencyTestBase
    {
        [Fact]
        public async Task AccountShouldBeAbleToGetTokensFromFaucet()
        {
            IWallet wallet = _serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            wallet = CreateFullWallet(wallet);

            await wallet.StoreMnemonicAsync(DEFAULT_MNEMONIC);

            (_, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");

            GenerateAddressesResponse generateAddressesResponse 
                = await account!.GenerateAddressesAsync();


            await account.SyncAccountAsync();
            GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
            long totalBalance = long.Parse(getBalanceResponse.Payload!.BaseCoin.Total);
            
            string address = generateAddressesResponse.Payload?.First()?.Address!;

            await account.RequestFromFaucetAsync(address, DEFAULT_FAUCET_URL);

            Thread.Sleep(20000);
            await account.SyncAccountAsync();
            getBalanceResponse = await account.GetBalanceAsync();

            long newBalance = long.Parse(getBalanceResponse.Payload!.BaseCoin.Total);

            newBalance.Should().BeGreaterThan(totalBalance);
        }
    }
}
