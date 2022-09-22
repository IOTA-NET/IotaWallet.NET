namespace IotaWalletNet.Options
{
    public class ClientOptions
    {
        public List<string> Nodes { get; set; } = new List<string>() { "https://api.testnet.shimmer.network" };

        public bool LocalPow { get; set; } = false;

        public bool FallbackToLocalPow { get; set; } = true;

        public bool Offline { get; set; } = false;

    }
}
