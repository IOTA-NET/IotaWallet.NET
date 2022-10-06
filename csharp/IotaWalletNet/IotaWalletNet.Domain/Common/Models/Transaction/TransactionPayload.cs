namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    public class TransactionPayload
    {
        public TransactionPayload(TransactionEssence essence)
        {
            Essence = essence;
        }

        /// <summary>
        /// The index namae
        /// </summary>
        public TransactionEssence Essence { get; set; }

        //TODO unlock types[]
    }
}
