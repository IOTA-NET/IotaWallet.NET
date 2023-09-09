using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Main.Examples.Accounts_and_Addresses;
using IotaWalletNet.Main.Examples.Accounts_and_Addresses.Check_Balance;
using IotaWalletNet.Main.Examples.Accounts_and_Addresses.Generate_an_Address;
using IotaWalletNet.Main.Examples.Events.Subscribe;
using IotaWalletNet.Main.Examples.Events.WaitForTransactionConfirmation;
using IotaWalletNet.Main.Examples.Native_Tokens.Melt;
using IotaWalletNet.Main.Examples.Native_Tokens.Mint;
using IotaWalletNet.Main.Examples.Native_Tokens.Send;
using IotaWalletNet.Main.Examples.NFTs.Burn;
using IotaWalletNet.Main.Examples.NFTs.Mint;
using IotaWalletNet.Main.Examples.NFTs.Send;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Periodic_Syncing;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Request_Tokens_from_Faucet;
using IotaWalletNet.Main.Examples.Outputs_and_Transactions.Send_a_Transaction;
using Newtonsoft.Json;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            //await CreateAWalletAndAccountExample.Run();

            //await RequestTokensFromFaucetExample.Run();

            await CheckBalanceExample.Run();

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