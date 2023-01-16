using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes
{
    public class BuildBasicOutputData
    {
        public BuildBasicOutputData(string? amount, NativeToken? nativeTokens, List<IUnlockConditionType> unlockConditions, List<IFeatureType>? features)
        {
            Amount = amount;
            NativeTokens = nativeTokens;
            UnlockConditions = unlockConditions;
            Features = features;
        }

        /// <summary>
        /// If not provided, minimum storage deposit will be used
        /// </summary>
        public string? Amount { get; set; }

        public NativeToken? NativeTokens { get; set; }

        public List<IUnlockConditionType> UnlockConditions{ get; set; }

        public List<IFeatureType>? Features { get; set; }
    }
}
