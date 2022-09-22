
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

            ;
            Console.Read();
        }
    }
}