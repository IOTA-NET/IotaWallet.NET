namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressWithNativeTokens
    {
        public AddressWithNativeTokens(List<string[]> tokenIdWithAmounts, string address)
        {
            NativeTokens = tokenIdWithAmounts;
            Address = address;
        }


        /// <summary>
        /// A list of (string tokenId, string hexEncodedAmount) 
        /// </summary>
        public List<string[]> NativeTokens { get; set; }

        public string Address { get; set; }

        public string? ReturnAddress { get; set; }

        public ulong? Expiration { get; set; }
    }
}
