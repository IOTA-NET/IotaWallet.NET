using IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{

    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(ConsolidationRequiredWalletEventType), "ConsolidationRequired")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(NewOutputWalletEventType), "NewOutput")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(SpentOutputWalletEventType), "SpentOutput")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(TransactionInclusionWalletEventType), "TransactionInclusion")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(TransactionProgressWalletEventType), "TransactionProgress")]
    public interface IWalletEventType
    {
        public string Type { get; set; }
    }
}