using IotaWalletNet.Domain.Common.Models.Output.FeatureTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(IssuerFeature), 1)]
    [JsonSubtypes.KnownSubType(typeof(MetadataFeature), 2)]
    [JsonSubtypes.KnownSubType(typeof(SenderFeature), 0)]
    [JsonSubtypes.KnownSubType(typeof(TagFeature), 3)]
    public interface IFeatureType
    {
        public int Type { get; }
    }
}
