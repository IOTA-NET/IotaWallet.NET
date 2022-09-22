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
}
