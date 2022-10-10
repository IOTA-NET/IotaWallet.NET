using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class GovernorAddressUnlockCondition : IUnlockConditionType
    {
        public GovernorAddressUnlockCondition(IAddressType addressType)
        {
            Address = addressType;
        }

        public int Type { get; } = 5;

        /// <summary>
        /// The Address
        /// </summary>
        public IAddressType Address { get; set; }
    }
}
