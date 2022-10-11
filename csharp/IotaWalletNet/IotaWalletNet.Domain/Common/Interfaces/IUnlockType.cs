using IotaWalletNet.Domain.Common.Models.Output.UnlockTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(AliasUnlock), 2)]
    [JsonSubtypes.KnownSubType(typeof(NftUnlock), 3)]
    [JsonSubtypes.KnownSubType(typeof(ReferenceUnlock), 1)]
    [JsonSubtypes.KnownSubType(typeof(SignatureUnlock), 0)]
    public interface IUnlockType
    {
        public int Type { get; }
    }
}
