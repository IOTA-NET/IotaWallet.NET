using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class ExpirationUnlockCondition : IUnlockConditionType
    {
        public ExpirationUnlockCondition(IAddressType address, ulong unixTime)
        {
            Address = address;
            UnixTime = unixTime;
        }

        public int Type { get; } = 3;

        /// <summary>
        /// The return address
        /// </summary>
        public IAddressType Address { get; set; }

        /// <summary>
        /// Before this unix time, the condition is allowed to unlock the output, after that only the address defined in return address.
        /// </summary>
        public ulong UnixTime { get; set; }
    }
}
