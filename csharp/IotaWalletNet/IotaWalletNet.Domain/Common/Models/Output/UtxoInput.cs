namespace IotaWalletNet.Domain.Common.Models.Output
{
    public class UtxoInput
    {
        /// <summary>
        /// The transaction id
        /// </summary>
        public string TransactionId { get; set; }


        /// <summary>
        /// The output index
        /// </summary>
        public uint TransactionOutputIndex { get; set; }
    }
}
