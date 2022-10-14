using IotaWalletNet.Main.Examples.NFTs.Burn;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CreateAWalletAndAccountExample.Run();

            //await RequestTokensFromFaucetExample.Run();

            //await CheckBalanceExample.Run();
            //await SendTransactionExample.Run();

            //await GenerateAnAddressExample.Run();
            //await MintNftExample.Run();

            //await SendNftExample.Run();

            await BurnNftExample.Run();
        }
    }
}