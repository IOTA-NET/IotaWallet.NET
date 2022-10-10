using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Address.AddressTypes
{
    public class AliasAddress : IAddressType
    {
        public AliasAddress(string aliasId)
        {
            AliasId = aliasId;
        }

        public int Type { get; } = 8;

        /// <summary>
        /// [HexEncoded] The alias id.
        /// </summary>
        public string AliasId { get; set; }
    }
}
