using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(MilestonePayload), 7)]
    [JsonSubtypes.KnownSubType(typeof(TaggedDataPayload), 5)]
    [JsonSubtypes.KnownSubType(typeof(TransactionPayload), 6)]
    [JsonSubtypes.KnownSubType(typeof(TreasuryTransactionPayload), 4)]
    public interface IPayloadType
    {
        public int Type { get; }
    }
}
