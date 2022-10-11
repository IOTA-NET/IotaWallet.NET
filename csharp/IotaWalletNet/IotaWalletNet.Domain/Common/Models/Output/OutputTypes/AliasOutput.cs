using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputTypes
{
    public class AliasOutput : CommonOutput, IOutputType
    {
        public AliasOutput(string amount, string aliasId, List<IUnlockConditionType> unlockConditions)
            : base(unlockConditions)
        {
            Amount = amount;
            AliasId = aliasId;
        }

        public int Type { get; } = 4;

        /// <summary>
        /// The amount of IOTA coins to held by the output. 
        /// </summary>
        public string Amount { get; set; }


        /// <summary>
        /// [Hexencoded] Unique identifier of the alias, which is the BLAKE2b-160 hash of the Output ID that created it.
        /// 
        /// </summary>
        public string AliasId { get; set; }

        /// <summary>
        /// A counter that must increase by 1 every time the alias is state transitioned.
        /// </summary>
        public ulong stateIndex { get; set; }


        /// <summary>
        /// Metadata that can only be changed by the state controller.
        /// </summary>
        public string? StateMetadata { get; set; }

        /// <summary>
        /// A counter that denotes the number of foundries created by this alias account.
        /// </summary>
        public ulong FoundryCounter { get; set; }


        /// <summary>
        /// Immutable features contained by the output.
        /// </summary>
        public List<IFeatureType>? ImmutableFeatures { get; set; } = null;

    }
}
