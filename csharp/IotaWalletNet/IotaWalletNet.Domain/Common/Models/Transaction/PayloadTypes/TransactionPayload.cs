using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Transaction.EssenceTypes;

namespace IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes
{
    public class TransactionPayload : IPayloadType
    {
        public TransactionPayload(TransactionEssence essence, List<IUnlockType> unlocks)
        {
            Essence = essence;
            Unlocks = unlocks;
        }

        public int Type { get; } = 6;
        /// <summary>
        /// The index namae
        /// </summary>
        public TransactionEssence Essence { get; set; }

        public List<IUnlockType> Unlocks { get; set; }
    }
}
