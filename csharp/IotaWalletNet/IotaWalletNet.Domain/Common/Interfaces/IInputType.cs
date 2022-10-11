using IotaWalletNet.Domain.Common.Models.Output.InputTypes;
using IotaWalletNet.Domain.Common.Models.Output.OutputTypes;
using JsonSubTypes;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Interfaces
{

    [JsonConverter(typeof(JsonSubtypes), "Type")]
    [JsonSubtypes.KnownSubType(typeof(UtxoInput), 0)]
    [JsonSubtypes.KnownSubType(typeof(TreasuryInput), 1)]
    internal interface IInputType
    {
        public int Type { get; }    
    }
}
