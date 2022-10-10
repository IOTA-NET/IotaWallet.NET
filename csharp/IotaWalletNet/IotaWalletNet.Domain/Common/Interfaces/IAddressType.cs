using IotaWalletNet.Domain.Common.Models.Address.AddressTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(AliasAddress), 8)]
    [JsonSubtypes.KnownSubType(typeof(Ed25519Address), 0)]
    [JsonSubtypes.KnownSubType(typeof(NftAddress), 16)]
    public interface IAddressType
    {
        public int Type { get; }
    }
}
