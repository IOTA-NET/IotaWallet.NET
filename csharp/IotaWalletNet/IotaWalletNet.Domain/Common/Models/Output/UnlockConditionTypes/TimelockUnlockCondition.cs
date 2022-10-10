using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class TimelockUnlockCondition : IUnlockConditionType
    {
        public TimelockUnlockCondition()
        {

        }

        public int Type { get; } = 2;

        /// <summary>
        /// Unix time (seconds since Unix epoch) starting from which the output can be consumed.
        /// </summary>
        public ulong UnixTime { get; set; }
    }
}
