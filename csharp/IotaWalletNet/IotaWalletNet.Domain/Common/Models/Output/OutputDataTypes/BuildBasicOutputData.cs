using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes
{
    public class BuildBasicOutputData
    {
        /// <summary>
        /// If not provided, minimum storage deposit will be used

        /// </summary>
        public string? Amount { get; set; }

        public NativeToken? NativeTokens { get; set; }

        public List<IUnlockConditionType> UnlockConditions { get; set; } = new List<IUnlockConditionType>();

        public List<IFeatureType> Features { get; set; } = new List<IFeatureType>();
    }
}
