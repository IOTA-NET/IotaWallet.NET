using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes
{
    public class BuildNftOutputData : BuildBasicOutputData
    {
        public BuildNftOutputData(string? amount, NativeToken? nativeTokens, List<IUnlockConditionType> unlockConditions, List<IFeatureType>? features, List<IFeatureType>? immutableFeatures, string? nftId)
            : base(amount, nativeTokens, unlockConditions, features)
        {
            ImmutableFeatures = immutableFeatures;
            NftId = nftId;
        }

        /// <summary>
        /// If not provided, minimum storage deposit will be used
        /// </summary>
        public string? NftId { get; set; } = string.Empty;

        public List<IFeatureType>? ImmutableFeatures { get; set; }
    }
}
