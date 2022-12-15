using IotaWalletNet.Main.Examples.Accounts_and_Addresses;
using IotaWalletNet.Main.Examples.Events;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Request_Tokens_from_Faucet;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //await CreateAWalletAndAccountExample.Run();

            //await RequestTokensFromFaucetExample.Run();

            //await CheckBalanceExample.Run();
            //await SendTransactionExample.Run();

            //await GenerateAnAddressExample.Run();
            //await MintNftExample.Run();

            //await SendNftExample.Run();

            //await BurnNftExample.Run();

            //await MintNativeTokensExample.Run();

            //await SendNativeTokensExample.Run();

            await EventsExample.Run();
        }
    }
}