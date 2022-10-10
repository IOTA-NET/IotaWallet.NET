using IotaWalletNet.Domain.Common.Models.Coin.TokenSchemeTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(SimpleTokenScheme), 0)]
    public interface ITokenSchemeType
    {
        public int Type { get; }
    }
}
