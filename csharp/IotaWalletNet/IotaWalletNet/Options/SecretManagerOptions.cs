using IotaWalletNet.SerializerSettings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IotaWalletNet.Options
{
    public class SecretManagerOptions
    {
        //As this property name is case-sensitive to be pascal case, whereas the defaults is camelCase
        [JsonProperty(PropertyName = "Stronghold", NamingStrategyType = typeof(PascalCaseNamingStrategy))]
        public StrongholdOptions Stronghold { get; set; } = new StrongholdOptions();

    }

    public class SecretManagerOptionsBuilder
    {
        private readonly Wallet _wallet;
        private StrongholdOptions _strongholdOptions;

        public SecretManagerOptionsBuilder(Wallet wallet)
        {
            _wallet = wallet;
            _strongholdOptions = _wallet.GetWalletOptions().SecretManager.Stronghold;
        }

        public SecretManagerOptionsBuilder SetPassword(string password)
        {
            _strongholdOptions.Password = password;

            return this;
        }

        public SecretManagerOptionsBuilder SetSnapshotPath(string snapshotPath)
        {
            _strongholdOptions.SnapshotPath = snapshotPath;

            return this;
        }

        public Wallet ThenBuild() => _wallet;
    }
}
