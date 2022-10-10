using IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(AddressUnlockCondition), 0)]
    [JsonSubtypes.KnownSubType(typeof(StorageDepositReturnUnlockCondition), 1)]
    [JsonSubtypes.KnownSubType(typeof(TimelockUnlockCondition), 2)]
    [JsonSubtypes.KnownSubType(typeof(ExpirationUnlockCondition), 3)]
    [JsonSubtypes.KnownSubType(typeof(StateControllerAddressUnlockCondition), 4)]
    [JsonSubtypes.KnownSubType(typeof(GovernorAddressUnlockCondition), 5)]
    [JsonSubtypes.KnownSubType(typeof(ImmutableAliasUnlockCondition), 6)]
    public interface IUnlockConditionType
    {
        public int Type { get; }
    }
}
