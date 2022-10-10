using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputTypes
{
    public class FoundryOutput : CommonOutput, IOutputType
    {
        public FoundryOutput(string amount, ITokenSchemeType tokenSchemeType, List<IUnlockConditionType> unlockConditions)
            : base(unlockConditions)
        {
            Amount = amount;
            TokenScheme = tokenSchemeType;
        }

        public int Type { get; } = 5;

        /// <summary>
        /// The amount of IOTA tokens held by the output. 
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// The serial number of the foundry with respect to the controlling alias.
        /// </summary>
        public ulong SerialNumber { get; set; }

        /// <summary>
        /// The token scheme for the foundry.
        /// </summary>
        public ITokenSchemeType TokenScheme { get; set; }

        /// <summary>
        /// Immutable features contained by the output.
        /// </summary>
        public List<IFeatureType>? ImmutableFeatures { get; set; }
    }
}
