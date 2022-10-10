using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class StateControllerAddressUnlockCondition : IUnlockConditionType
    {
        public StateControllerAddressUnlockCondition(IAddressType addressType)
        {
            Address = addressType;
        }

        public int Type { get; } = 4;

        /// <summary>
        /// The Address
        /// </summary>
        public IAddressType Address { get; set; }
    }
}
