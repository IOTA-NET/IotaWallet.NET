using IotaWalletNet.Main.Examples.Accounts_and_Addresses;
using IotaWalletNet.Main.Examples.Accounts_and_Addresses.Check_Balance;
using IotaWalletNet.Main.Examples.Events.WaitForTransactionConfirmation;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Periodic_Syncing;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Request_Tokens_from_Faucet;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            //await CreateAWalletAndAccountExample.Run();

            await RequestTokensFromFaucetExample.Run();

            //await CheckBalanceExample.Run();

            //await SendTransactionExample.Run();

            //await SendMicroTransactionExample.Run();

            //await GenerateAnAddressExample.Run();

            //await MintNftExample.Run();

            //await SendNftExample.Run();

            //await BurnNftExample.Run();

            //await MintNativeTokensExample.Run();

            //await SendNativeTokensExample.Run();

            //await MeltNativeTokensExample.Run();

            //await BurnNativeTokensExample.Run();
            //await EventsExample.Run();

            //await ClaimOutputsExample.Run();

            //await PeriodicSyncingExample.Run();
            //await WaitForTransactionConfirmationExample.Run();
        }

    }

}