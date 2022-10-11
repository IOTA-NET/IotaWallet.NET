using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockTypes
{
    public class ReferenceUnlock : IUnlockType
    {
        public ReferenceUnlock()
        {
        }

        public int Type { get; } = 1;

        /// <summary>
        /// The reference
        /// </summary>
        public ulong Reference { get; set; }
    }
}
