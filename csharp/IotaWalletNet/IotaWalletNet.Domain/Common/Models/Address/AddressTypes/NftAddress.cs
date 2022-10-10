using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Address.AddressTypes
{
    public class NftAddress : IAddressType
    {
        public NftAddress(string nftId)
        {
            NftId = nftId;
        }

        public int Type { get; } = 16;

        /// <summary>
        /// [HexEncoded] The NFT Id.
        /// </summary>
        public string NftId { get; set; }
    }
}
