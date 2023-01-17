using IotaWalletNet.Domain.Common.Interfaces;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BasicOutput : CommonOutput, IOutputType
    {
        public BasicOutput(string amount, List<IUnlockConditionType> unlockConditions)
            : base(unlockConditions)
        {
            Amount = amount;
        }

        public int Type { get; } = 3;

        /// <summary>
        /// The amount of IOTA coins to held by the output. 
        /// </summary>
        public string Amount { get; set; }
    }
}
