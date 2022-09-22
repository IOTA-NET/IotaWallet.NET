
namespace IotaWalletNet.Testbed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Wallet wallet = new Wallet()
                        .ConfigureWalletOptions()
                            .SetCoinType(Options.WalletOptions.TypeOfCoin.Shimmer)
                            .SetStoragePath("./walletdb")
                            .ThenBuild()
                        .ConfigureClientOptions()
                            .IsOffline(false)
                            .IsFallbackToLocalPow()
                            .IsLocalPow(false)
                            .ThenBuild()
                        .ConfigureSecretManagerOptions()
                            .SetPassword("password")
                            .SetSnapshotPath("./mystronghold")
                            .ThenBuild();

            wallet.Connect();

            wallet.SendMessage(@"
                {
                    ""cmd"": ""StoreMnemonic"",
                    ""payload"": ""sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf""
                }
            ");
            Console.Read();
        }
    }
}