using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Crypto.SignatureTypes;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockTypes
{
    public class SignatureUnlock : IUnlockType
    {
        public SignatureUnlock(Ed25519Signature signature)
        {
            Signature = signature;
        }

        public int Type { get; } = 0;

        /// <summary>
        /// The signature.
        /// </summary>
        public Ed25519Signature Signature { get; set; }
    }
}
