namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class TokenIdWithAmount
    {

        public TokenIdWithAmount(string tokenID, string amount)
        {
            TokenID = tokenID;
            Amount = amount;
        }

        public string TokenID { get; set; }

        /// <summary>
        /// [HexEncoded Amount]
        /// </summary>
        public string Amount { get; set; }
    }
}
