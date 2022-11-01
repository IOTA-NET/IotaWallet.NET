namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressWithNativeTokens
    {
        public AddressWithNativeTokens(List<TokenIdWithAmount> tokenIdWithAmounts, string address)
        {
            TokenIdWithAmounts = tokenIdWithAmounts;
            Address = address;
        }


        public List<TokenIdWithAmount> TokenIdWithAmounts { get; set; }

        public string Address { get; set; }

        public string? ReturnAddress { get; set; }

        public ulong? Expiration { get; set; }
    }

    public class TokenIdWithAmount
    {
        public string TokenID { get; set; }

        /// <summary>
        /// [HexEncoded Amount]
        /// </summary>
        public string Amount { get; set; }
    }
}
