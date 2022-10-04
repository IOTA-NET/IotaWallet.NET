namespace IotaWalletNet.Domain.Common.Models.Account
{
    public class BaseCoinBalance
    {
        public BaseCoinBalance(string total, string available)
        {
            Total = total;
            Available = available;
        }

        /// <summary>
        /// The total amount of all the outputs
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// The amount in all the outputs that aren't used in a transaction
        /// </summary>
        public string Available { get; set; }
    }
}
