
namespace IotaWalletNet.Testbed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string newMnemonic = SecretManager.GenerateNewMnemonic();
            string password = "password";

            SecretManager secretManager = new SecretManager()
                                            .SetPassword(password)
                                            .InitializeStronghold();
                                            //.StoreMnemonic(newMnemonic);
                                                

            WalletManager walletManager = new WalletManager()
                                                .SetSecretManager(secretManager)
                                                .SetCoinType(WalletManager.CoinType.SHIMMER)
                                                .SetNodeUrl(WalletManager.DEFAULT_NODE_URL)
                                                .Connect();

            //walletManager.CreateAccount("zayyan");

            List<string> usernames = walletManager.GetUsernames();
            usernames.ForEach(username => Console.WriteLine(username));

            Console.WriteLine("End of program...");
            Console.Read();
        }
    }
}