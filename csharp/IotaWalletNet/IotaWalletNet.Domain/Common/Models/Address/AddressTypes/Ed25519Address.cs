using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Address.AddressTypes
{
    public class Ed25519Address : IAddressType
    {
        public Ed25519Address(string pubKeyHash)
        {
            PubKeyHash = pubKeyHash;
        }

        public int Type { get; } = 0;

        /// <summary>
        /// The public key hash.
        /// </summary>
        public string PubKeyHash { get; set; }
    }
}
