namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressAndNftId
    {
        public AddressAndNftId(string address, string nftId)
        {
            Address = address;
            NftId = nftId;
        }

        public string Address { get; set; }

        public string NftId { get; set; }
    }
}
