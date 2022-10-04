namespace IotaWalletNet.Domain.Common.Models.Account
{
    public class NativeTokenBalance
    {
        public NativeTokenBalance(string tokenId, string total, string available)
        {
            TokenId = tokenId;
            Total = total;
            Available = available;
        }


        public string TokenId { get; set; }

        /// <summary>
        /// Hexencoded total
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Hexencoded available
        /// </summary>
        public string Available { get; set; }
    }
}
