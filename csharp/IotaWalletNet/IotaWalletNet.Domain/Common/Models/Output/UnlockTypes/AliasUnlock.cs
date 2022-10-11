using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockTypes
{
    public class AliasUnlock : IUnlockType
    {
        public AliasUnlock()
        {
        }

        public int Type { get; } = 2;

        /// <summary>
        /// The reference
        /// </summary>
        public ulong Reference { get; set; }
    }
}
