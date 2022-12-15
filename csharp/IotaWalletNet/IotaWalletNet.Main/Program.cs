using IotaWalletNet.Main.Examples.Events.Subscribe;

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