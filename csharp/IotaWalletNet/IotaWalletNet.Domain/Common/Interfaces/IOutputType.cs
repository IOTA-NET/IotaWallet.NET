using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Domain.Common.Models.Output.OutputTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(AliasOutput), 4)]
    [JsonSubtypes.KnownSubType(typeof(BasicOutput), 3)]
    [JsonSubtypes.KnownSubType(typeof(FoundryOutput), 5)]
    [JsonSubtypes.KnownSubType(typeof(NftOutput), 6)]
    [JsonSubtypes.KnownSubType(typeof(TreasuryOutput), 2)]
    public interface IOutputType
    {
        public int Type { get; }
    }
}
