using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.InputTypes
{
    public class UtxoInput : IInputType
    {
        public UtxoInput(string transactionId, uint transactionOutputIndex)
        {
            TransactionId = transactionId;
            TransactionOutputIndex = transactionOutputIndex;
        }

        public int Type { get; } = 0;

        /// <summary>
        /// [HexEncoded] The transaction id
        /// </summary>
        public string TransactionId { get; set; }


        /// <summary>
        /// The output index
        /// </summary>
        public uint TransactionOutputIndex { get; set; }
    }
}
