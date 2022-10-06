namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    public class Transaction
    {
        public Transaction(TransactionPayload payload, string blockId, string timestamp, string transactionId, string networkId, bool incoming)
        {
            Payload = payload;
            BlockId = blockId;
            Timestamp = timestamp;
            TransactionId = transactionId;
            NetworkId = networkId;
            Incoming = incoming;
        }

        /// <summary>
        /// The transaction payload
        /// </summary>
        public TransactionPayload Payload { get; set; }

        /// <summary>
        /// The block id in which the transaction payload was included
        /// </summary>
        public string BlockId { get; set; }

        /// <summary>
        /// The creation time
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// The transaction id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// The network id in which the transaction was sent
        /// </summary>
        public string NetworkId { get; set; }

        /// <summary>
        /// If the transaction was created by the wallet or someone else
        /// </summary>
        public bool Incoming { get; set; }

        public string? Note { get; set; }
    }
}
