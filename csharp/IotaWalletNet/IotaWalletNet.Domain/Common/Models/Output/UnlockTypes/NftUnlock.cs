using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockTypes
{
    public class NftUnlock : IUnlockType
    {
        public NftUnlock()
        {
        }

        public int Type { get; } = 3;

        /// <summary>
        /// The reference
        /// </summary>
        public ulong Reference { get; set; }
    }
}
