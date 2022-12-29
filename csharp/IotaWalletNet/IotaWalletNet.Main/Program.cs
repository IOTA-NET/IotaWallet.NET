using IotaWalletNet.Main.Examples.Native_Tokens.Melt;
using IotaWalletNet.Main.Examples.Native_Tokens.Mint;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Send_a_Transaction;

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

            //await MeltNativeTokensExample.Run();

            await BurnNativeTokensExample.Run();
            //await EventsExample.Run();
        }

    }

}