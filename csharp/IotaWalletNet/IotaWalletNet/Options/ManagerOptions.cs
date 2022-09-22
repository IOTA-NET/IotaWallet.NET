using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace IotaWalletNet.Options
{
    public class ManagerOptions
    {
        private SecretManagerOptions _secretManagerOptions;
        private ClientOptions _clientConfigOptions = new ClientOptions();

        public string StoragePath { get; set; } = "./walletdb";

        public int CoinType { get; set; } = 4219;

        public ManagerOptions()
        {
            SecretManager = new SecretManagerOptions();
            ClientConfigOptions = new ClientOptions();
        }

        [JsonProperty(PropertyName = "secretManager")]
        public string SecretManagerJson { get; set; } = string.Empty;


        [JsonIgnore]
        public SecretManagerOptions SecretManager
        {
            get => _secretManagerOptions;
            set
            {
                _secretManagerOptions = value;

                SecretManagerJson = JsonConvert.SerializeObject(SecretManager);

                ////iota.rs is particular on pascal case for stronghold
                //if (SecretManagerJson.Contains("stronghold"))
                //    SecretManagerJson = SecretManagerJson.Replace("stronghold", "Stronghold");
            }
        }

        #region ClientOptions

        [JsonProperty(PropertyName = "clientOptions")]
        public string ClientOptionsJson { get; set; } = string.Empty;

        [JsonIgnore]
        public ClientOptions ClientConfigOptions
        {
            get => _clientConfigOptions;
            set
            {
                _clientConfigOptions = value;
                ClientOptionsJson = JsonConvert.SerializeObject(_clientConfigOptions);

            }
        }

        #endregion
    }
}
