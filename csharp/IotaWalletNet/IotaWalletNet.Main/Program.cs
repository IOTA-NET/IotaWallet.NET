﻿using IotaWalletNet.Main.Examples.Accounts_and_Addresses;
using IotaWalletNet.Main.Examples.Accounts_and_Addresses.Generate_an_Address;

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