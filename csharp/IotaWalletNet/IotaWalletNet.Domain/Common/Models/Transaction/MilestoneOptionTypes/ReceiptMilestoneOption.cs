using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Transaction.MilestoneOptionTypes
{
    public class ReceiptMilestoneOption : IMilestoneOptionType
    {
        public int Type { get; } = 0;

        /// <summary>
        /// The milestone index at which the funds were migrated in the legacy network.
        /// </summary>
        public ulong MigratedAt { get; set; }


        /// <summary>
        /// Whether this Receipt is the final one for a given migrated at index.
        /// </summary>
        public bool Final { get; set; }

        //funds: IMigratedFunds[];
        public Trea MyProperty { get; set; }
    }
}
