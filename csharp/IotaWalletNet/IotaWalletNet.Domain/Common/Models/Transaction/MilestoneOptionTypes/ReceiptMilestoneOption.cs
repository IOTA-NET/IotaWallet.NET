using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;

namespace IotaWalletNet.Domain.Common.Models.Transaction.MilestoneOptionTypes
{
    public class ReceiptMilestoneOption : IMilestoneOptionType
    {

        public ReceiptMilestoneOption(List<MigratedFunds> funds, TreasuryTransactionPayload transaction)
        {
            Funds = funds;
            Transaction = transaction;
        }

        public int Type { get; } = 0;

        /// <summary>
        /// The milestone index at which the funds were migrated in the legacy network.
        /// </summary>
        public ulong MigratedAt { get; set; }


        /// <summary>
        /// Whether this Receipt is the final one for a given migrated at index.
        /// </summary>
        public bool Final { get; set; }

        /// <summary>
        /// The index data.
        /// </summary>
        public List<MigratedFunds> Funds { get; set; }

        /// <summary>
        /// The TreasuryTransaction used to fund the funds.
        /// </summary>
        public TreasuryTransactionPayload Transaction { get; set; }
    }
}
