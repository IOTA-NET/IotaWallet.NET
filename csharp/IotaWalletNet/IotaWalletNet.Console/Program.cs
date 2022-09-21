
namespace IotaWalletNet.Testbed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string newMnemonic = SecretManager.GenerateNewMnemonic();
            string password = "password";

            SecretManager secretManager = new SecretManager(password)
                                                .StoreMnemonic(newMnemonic);


            Console.WriteLine("End of program...");
            Console.Read();
        }
    }
}