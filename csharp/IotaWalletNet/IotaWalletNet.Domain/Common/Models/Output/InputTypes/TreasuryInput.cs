using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.InputTypes
{
    public class TreasuryInput : IInputType
    {
        public TreasuryInput(string milestoneId)
        {
            MilestoneId = milestoneId;
        }

        public int Type { get; } = 1;

        /// <summary>
        /// [HexEncoded] The milestone id of the input.
        /// </summary>
        public string MilestoneId { get; set; }
    }
}
