using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputTypes
{
    public class NftOutput : CommonOutput, IOutputType
    {
        public NftOutput(string amount, string nftId, List<IUnlockConditionType> unlockConditions)
            : base(unlockConditions)
        {
            Amount = amount;
            NftId = nftId;
        }

        public int Type { get; } = 6;

        /// <summary>
        /// The amount of IOTA tokens held by the output.
        /// </summary>
        public string Amount { get; set; }


        /// <summary>
        /// [HexEncoded] Unique identifier of the NFT, which is the BLAKE2b-160 hash of the Output ID that created it.
        /// </summary>
        public string NftId { get; set; }


        public List<IFeatureType>? ImmutableFeatures { get; set; } = null;
    }
}
