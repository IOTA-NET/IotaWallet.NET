using IotaWalletNet.Application.Common.Interfaces;

namespace IotaWalletNet.Application.Common.Options
{
    public class ClientOptions
    {
        public HashSet<string> Nodes { get; set; } = new HashSet<string>();

        public bool LocalPow { get; set; } = true;

        public bool FallbackToLocalPow { get; set; } = true;

        public string FaucetUrl { get; set; } = "https://faucet.testnet.shimmer.network";
    }

    public class ClientOptionsBuilder
    {
        private readonly IWallet _wallet;
        private readonly ClientOptions _clientOptions;

        public ClientOptionsBuilder(IWallet wallet)
        {
            _wallet = wallet;
            _clientOptions = _wallet.GetWalletOptions().ClientConfigOptions;
        }

        public ClientOptionsBuilder AddNodeUrl(string nodeUrl)
        {
            _clientOptions.Nodes.Add(nodeUrl);
            return this;
        }

        public ClientOptionsBuilder IsLocalPow(bool isLocalPow = true)
        {
            _clientOptions.LocalPow = isLocalPow;
            return this;
        }

        public ClientOptionsBuilder IsFallbackToLocalPow(bool isFallbackToLocalPow = true)
        {
            _clientOptions.FallbackToLocalPow = isFallbackToLocalPow;
            return this;
        }

        public ClientOptionsBuilder SetFaucetUrl(string faucetUrl)
        {
            _clientOptions.FaucetUrl = faucetUrl;
            return this;
        }

        public IWallet ThenBuild()
        {
            //To trigger json re-construction
            _wallet.GetWalletOptions().ClientConfigOptions = _clientOptions;
            return _wallet;
        }
    }
}
