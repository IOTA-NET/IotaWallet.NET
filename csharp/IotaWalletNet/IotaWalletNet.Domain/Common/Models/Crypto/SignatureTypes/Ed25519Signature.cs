using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Crypto.SignatureTypes
{
    public class Ed25519Signature : ISignatureType
    {
        public Ed25519Signature(string publicKey, string signature)
        {
            PublicKey = publicKey;
            Signature = signature;
        }

        public int Type { get; } = 0;

        /// <summary>
        /// The public key.
        /// </summary>
        public string PublicKey { get; set; }


        /// <summary>
        /// The signature
        /// </summary>
        public string Signature { get; set; }
    }
}
