using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static IotaWalletNet.Application.Common.Options.WalletOptions;

namespace IotaWalletNet.Application.Common.Options
{
    public class WalletOptionsBuilder
    {
        private readonly IWallet _wallet;
        private readonly WalletOptions _walletOptions;
        public WalletOptionsBuilder(IWallet wallet)
        {
            _wallet = wallet;
            _walletOptions = _wallet.GetWalletOptions();
        }

        public WalletOptionsBuilder SetStoragePath(string storagePath)
        {
            _walletOptions.StoragePath = storagePath;
            return this;
        }

        public WalletOptionsBuilder SetCoinType(TypeOfCoin coinType)
        {
            _walletOptions.CoinType = (int)coinType;
            return this;
        }

        public IWallet ThenBuild() => _wallet;
    }

    public class WalletOptions
    {
        private readonly SecretManagerOptions _secretManagerOptions;
        private readonly ClientOptions _clientConfigOptions = new ClientOptions();

        public string StoragePath { get; set; } = "./walletdb";

        public int CoinType { get; set; } = (int)TypeOfCoin.Shimmer;

        

        public WalletOptions()
        {
            SecretManager = new SecretManagerOptions();
            ClientConfigOptions = new ClientOptions();
        }

        public SecretManagerOptions SecretManager { get; set; }

        #region ClientOptions


        [JsonProperty(PropertyName = "clientOptions")]
        public ClientOptions ClientConfigOptions { get; set; }

        #endregion
    }
}
