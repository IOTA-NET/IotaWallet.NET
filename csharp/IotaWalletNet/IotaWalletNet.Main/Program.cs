using IotaWalletNet.Main.Examples.Accounts_and_Addresses;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Send_a_Transaction;
using Newtonsoft.Json;
using System.Dynamic;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CreateAWalletAndAccountExample.Run();

            await SendTransactionExample.Run();
        }
    }
}