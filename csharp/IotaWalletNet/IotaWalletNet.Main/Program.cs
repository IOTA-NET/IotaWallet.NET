using IotaWalletNet.Main.Examples.Accounts_and_Addresses;
using Newtonsoft.Json;
using System.Dynamic;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CreateAWalletAndAccountExample.Run();
        }
    }
}