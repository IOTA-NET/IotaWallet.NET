
namespace IotaWalletNet.Testbed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SecretManager secretManager = new SecretManager("password");

            Console.WriteLine("Hello, World!");
            Console.Read();
        }
    }
}