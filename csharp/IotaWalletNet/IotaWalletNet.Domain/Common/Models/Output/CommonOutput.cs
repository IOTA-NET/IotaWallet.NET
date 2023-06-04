using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using Newtonsoft.Json;

namespace IotaWalletNet.Domain.Common.Models.Output
{
    public abstract class CommonOutput
    {
        protected CommonOutput(List<IUnlockConditionType> unlockConditions)
        {
            UnlockConditions = unlockConditions;
        }

        /// <summary>
        /// The native tokens held by the output.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NativeToken>? NativeTokens { get; set; }

        /// <summary>
        /// The unlock conditions for the output.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        /// 
        public List<IUnlockConditionType> UnlockConditions { get; set; }

        /// <summary>
        /// Features containe d by the output.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<IFeatureType>? Features { get; set; }
    }
}
