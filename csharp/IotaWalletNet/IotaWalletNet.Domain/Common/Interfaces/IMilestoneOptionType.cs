using IotaWalletNet.Domain.Common.Models.Transaction.MilestoneOptionTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{

    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(ReceiptMilestoneOption), 0)]
    [JsonSubtypes.KnownSubType(typeof(ProtocolParamsMilestoneOption), 1)]
    public interface IMilestoneOptionType
    {
        public int Type { get; }
    }
}
