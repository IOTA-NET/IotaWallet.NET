using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Transaction.MilestoneOptionTypes
{
    public class ProtocolParamsMilestoneOption : IMilestoneOptionType
    {
        public ProtocolParamsMilestoneOption(string @params)
        {
            Params = @params;
        }

        public int Type { get; } = 1;

        /// <summary>
        /// The milestone index at which these protocol parameters become active.
        /// </summary>
        public ulong TargetMilestoneIndex { get; set; }

        /// <summary>
        /// The to be applied protocol version.
        /// </summary>
        public ulong ProtocolVersion { get; set; }

        /// <summary>
        /// The protocol parameters in binary form. Hex-encoded with 0x prefix.
        /// </summary>
        public string Params { get; set; }
    }
}
