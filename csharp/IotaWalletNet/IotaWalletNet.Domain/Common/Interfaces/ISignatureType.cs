using IotaWalletNet.Domain.Common.Models.Coin.TokenSchemeTypes;
using IotaWalletNet.Domain.Common.Models.Crypto.SignatureTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(Ed25519Signature), 0)]
    public interface ISignatureType
    {
        public int Type { get; }
    }
}
