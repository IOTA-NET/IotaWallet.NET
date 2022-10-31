namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    /// <summary>
    /// The result of a minting operation
    /// </summary>
    public class MintTokenTransaction
    {
        public MintTokenTransaction(string tokenId, Transaction transaction)
        {
            TokenId = tokenId;
            Transaction = transaction;
        }

        /// <summary>
        /// The token id of the minted token
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// The transaction which minted the token
        /// </summary>
        public Transaction Transaction { get; set; }
    }
}
