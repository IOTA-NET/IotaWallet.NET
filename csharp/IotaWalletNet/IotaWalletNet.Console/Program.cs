
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
                                                

            Wallet wallet = new Wallet()
                                                .SetSecretManager(secretManager)
                                                .SetCoinType(Wallet.CoinType.SHIMMER)
                                                .SetNodeUrl(Wallet.DEFAULT_NODE_URL)
                                                .Connect();

            //walletManager.CreateAccount("zayyan");

            List<string> usernames = wallet.GetUsernames();
            usernames.ForEach(username => Console.WriteLine(username));

            Account zanaAccount = wallet.GetAccount("zanamonster");

            Console.WriteLine("End of program...");
            Console.Read();
        }
    }
}