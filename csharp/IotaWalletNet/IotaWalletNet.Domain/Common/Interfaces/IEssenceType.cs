using IotaWalletNet.Domain.Common.Models.Transaction.EssenceTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(TransactionEssence), 1)]
    public interface IEssenceType
    {

        public int Type { get; }
    }
}
