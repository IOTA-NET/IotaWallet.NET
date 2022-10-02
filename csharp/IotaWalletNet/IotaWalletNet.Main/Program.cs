using IotaWalletNet.Main.Examples.Accounts_and_Addresses;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CreateAWalletAndAccountExample.Run();

            //await RequestTokensFromFaucetExample.Run();

            //await SendTransactionExample.Run();

            //await GenerateAnAddressExample.Run();

        }
    }
}