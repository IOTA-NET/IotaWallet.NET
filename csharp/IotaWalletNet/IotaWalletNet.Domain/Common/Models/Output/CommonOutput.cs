using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;

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
        public List<NativeToken>? NativeTokens { get; set; }

        /// <summary>
        /// The unlock conditions for the output.
        /// </summary>
        public List<IUnlockConditionType> UnlockConditions { get; set; }

        /// <summary>
        /// Features contained by the output.
        /// </summary>
        public List<IFeatureType>? Features { get; set; }
    }
}
